using System;
using System.Collections.Generic;
using System.Linq;
using Arithmetic_Interpreter_UWP;

namespace JymlRuntime {
    /// <summary>
    /// 代表解释器的运行时环境。
    /// 整个解释器的生命周期只能有一个 environment ，这就是为什么要把它弄成静态的原因。
    /// 它需要在整个解释器生命周期中维持 _eid, _enviromentList, EnvironmentNode 的状态。
    /// </summary>
    public static class Environment {
        /// <summary>
        /// 环境节点索引
        /// </summary>
        static private int _eid;
        /// <summary>
        /// 存储运行时环境注册的所有 EnvironmentNode，其包含了 Eid 和 ObjectTable。
        /// </summary>
        static private LinkedList<EnvironmentNode> _enviromentList = new LinkedList<EnvironmentNode>();
        /// <summary>
        /// 表示解释器的环境节点
        /// </summary>
		public class EnvironmentNode {
            /// <summary>
            /// 当前环境节点的索引
            /// </summary>
			public int Eid { get; set; }
            /// <summary>
            /// 当前环境节点存储的所有对象表
            /// </summary>
			public LinkedList<ObjectTable> ObjectTables { get; set; } = new LinkedList<ObjectTable>();
            /// <summary>
            /// 构造一个环境节点
            /// </summary>
            /// <param name="eid"></param>
            /// <param name="table"></param>
			public EnvironmentNode(int eid, ObjectTable table) {
                this.Eid = eid;
                if (this.ObjectTables.Count == 0) {
                    this.ObjectTables.AddFirst(table);
                }
                else {
                    this.ObjectTables.AddLast(table);
                }
            }
        }
        /// <summary>
        /// 往运行时环境添加一个对象表
        /// </summary>
        /// <param name="table"></param>
        static public void AddTable(ObjectTable table) {
            Environment._eid += 1;
            Environment._enviromentList.AddAfter(
                    Environment._enviromentList.Last,
                    new EnvironmentNode(_eid, table)
            );
        }


        /// <summary>
        /// 获取运行时环境的所有对象表
        /// </summary>
        static public LinkedList<EnvironmentNode> GetAllObjectTables => Environment._enviromentList;
        /// <summary>
        /// 提取最新添加的环境节点的索引
        /// </summary>
        static public int TailEid => Environment._eid;
    }

    /// <summary>
    /// 表示一个对象表实例
    /// </summary>
    /// <remarks>
    /// 需要注意： Address 是一个内部类，由 make-address 返回,
    /// 其带有一个参数和两个来自 make-address 的环境变量。
    /// environment-id 在测试期间需要手动指定，当 object-table
    /// 真正添加到 environment 时，需要采取自动化措施填写该参数。
    /// </remarks>
    public class ObjectTable {
        private int EnvironmentId { get; set; }
        private int RowIndex { get; set; } = -1;

        public ObjectTable(int environentId) {
            this.EnvironmentId = environentId;
        }

        /// <summary>
        /// 对象表
        /// </summary>
        private static Stack<ObjectTable.JymlObject> _objects = null;
        /// <summary>
        /// 表示对象实体在运行时环境中的地址
        /// </summary>
        public class Address : IEquatable<Address> {
            public int Eid { get; set; }
            public int Row { get; set; }

            public Address(int eid, int row) {
                this.Eid = eid;
                this.Row = row;
            }

            public bool Equals(Address other) => this.Eid == other.Eid && this.Row == other.Row;

            public static bool operator ==(Address @this, Address other) => @this.Equals(other);

            public static bool operator !=(Address @this, Address other) => !(@this == other);

            public override bool Equals(object obj) {
                if (ReferenceEquals(this, obj)) {
                    return true;
                }

                if (ReferenceEquals(obj, null)) {
                    return false;
                }

                return false;
            }

            public override int GetHashCode() => base.GetHashCode();

            public override string ToString() => ObjectTable.AddressEncode(this);
        }
        /// <summary>
        /// 表示环境中的对象实体
        /// </summary>
        public class JymlObject {
            public Address Address { get; set; }
            public string Name { get; set; }
            public int Value { get; set; }
            public override string ToString() => $"{Address} {Name} {Value}";
        }
        /// <summary>
        /// 根据 eid 和 row 生成对象实体的地址
        /// </summary>
        /// <param name="eid"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public static Address MakeAddress(int eid, int row) => new Address(eid, row);
        /// <summary>
        /// 将字符串表示的地址解码为 Address 实例
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Address AddressDecode(string code) {
            if (code.Split('|').Length > 2) {
                throw new ArgumentException("地址编码非法", code);
            }
            else {
                return MakeAddress(Convert.ToInt32(code.Split('|')[0]),
                            Convert.ToInt32(code.Split('|')[1]));
            }
        }
        /// <summary>
        /// 将 Address 实例编码为字符串形式
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static string AddressEncode(Address address) =>
            address.Eid.ToString() + "|" + address.Row.ToString();
        /// <summary>
        /// 往对象表中添加对象实体
        /// </summary>
        /// <param name="name">对象在代码中的命名</param>
        /// <param name="value">对象值</param>
        /// <param name="addr">对象地址</param>
        public static void AddEntry(string name, int value, Address addr) =>
            _objects.UniquePush(new ObjectTable.JymlObject {
                Name = name,
                Value = value,
                Address = addr
            });
        /// <summary>
        /// 根据对象的命名在对象表中检索该对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ObjectTable.JymlObject GetObjectByName(string name) {
            var query = from obj in ObjectTable._objects
                        where obj.Name == name
                        select obj;
            if (query.Count() > 0) {
                return query.First<ObjectTable.JymlObject>();
            }
            else {
                throw new ArgumentException($"{name} 对象未定义");
            }
        }
        /// <summary>
        /// 根据对象的地址在对象表中检索该对象
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static ObjectTable.JymlObject GetObjectByAddress(Address address) {
            var query = from obj in ObjectTable._objects
                        where obj.Address == address
                        select obj;
            if (query.Count() == 0) {
                return query.First<ObjectTable.JymlObject>();
            }
            else {
                throw new ArgumentException($"对象未定义，无效地址: {ObjectTable.AddressEncode(address)}");
            }
        }
    }

    /// <summary>
    /// 扩展 System.Collections.Generic.Stack[T]
    /// </summary>
    static class StackExtended {
        /// <summary>
        /// 给 Stack 添加数据并作唯一性检查
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stack"></param>
        /// <param name="value"></param>
        public static void UniquePush<T>(this Stack<T> stack, T value) {
            if (stack.Contains(value)) {
                throw new ArgumentException("value of Stack<T> must be unique.");
            }
            else {
                stack.Push(value);
            }
        }
    }
}
