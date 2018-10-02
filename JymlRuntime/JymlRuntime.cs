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
		/// 环境节点下标
		/// </summary>
		static private int _eid;
		/// <summary>
		/// 存储对象表的 LinkedList, 每个节点是一个 EnvironmentNode，其包含了 Eid 和 ObjectTable。
		/// </summary>
		static private LinkedList<EnvironmentNode> _enviromentList = new LinkedList<EnvironmentNode>();

		public class EnvironmentNode {
			public int Eid { get; set; }
			public LinkedList<ObjectTable> ObjectTables { get; set; } = new LinkedList<ObjectTable>();

			public EnvironmentNode(int eid, ObjectTable table) {
				this.Eid = eid;
				if (this.ObjectTables.Count==0) {
					this.ObjectTables.AddFirst(table);
				}
				else {
					this.ObjectTables.AddLast(table);
				}
			}
		}

		static public void AddTable(ObjectTable table) {
			Environment._eid += 1;
			Environment._enviromentList.AddAfter(
					Environment._enviromentList.Last,
					new EnvironmentNode(_eid, table)
			);
		}


		/// <summary>
		/// 存储对象表的 LinkedList, 每个节点是一个 EnvironmentNode，其包含了 Eid 和 ObjectTable。
		/// </summary>
		static public LinkedList<EnvironmentNode> ObjectTables => Environment._enviromentList;
		/// <summary>
		/// 提取最近添加的环境节点下标
		/// </summary>
		static public int TailEid => Environment._eid;
	}

	/// <summary>
	/// 对象表
	/// 这里需要注意： address 是一个 proc，由 make-address 返回,
	/// 其带有一个参数和两个来自 make-address 的环境变量
	/// environment-id 在测试期间需要手动指定，当 object-table
	/// 真正添加到 environment 时，需要采取自动化措施填写该参数。
	/// </summary>
	public class ObjectTable {
		private int EnvironmentId { get; set; }
		private int RowIndex { get; set; } = -1;

		public ObjectTable(int environentId) {
			this.EnvironmentId = environentId;
		}

		/// <summary>
		/// 对象表
		/// </summary>
		private static Stack<ObjectTable.Object> _objects = null;

		public class Address : IEquatable<Address> {
			public int eid { get; set; }
			public int row { get; set; }

			public Address(int eid, int row) {
				this.eid = eid;
				this.row = row;
			}

			public override string ToString() => ObjectTable.AddressEncode(this);

			public bool Equals(Address other) {
				return this.eid == other.eid && this.row == other.row;
			}

			public static bool operator ==(Address @this, Address other) =>
				@this.Equals(other);

			public static bool operator !=(Address @this, Address other) =>
				!(@this == other);
		}

		public class Object {
			public Address Address { get; set; }
			public string Name { get; set; }
			public int Value { get; set; }
			public override string ToString() {
				return $"{Address} {Name} {Value}";
			}
		}

		public static Address MakeAddress(int eid, int row) => new Address(eid, row);

		public static Address AddressDecode(string code) {
			if (code.Split('|').Length > 2) {
				throw new ArgumentException("地址编码非法", code);
			}
			else {
				return MakeAddress(Convert.ToInt32(code.Split('|')[0]),
							Convert.ToInt32(code.Split('|')[1]));
			}
		}

		public static string AddressEncode(Address address) =>
			address.eid.ToString() + "|" + address.row.ToString();

		public static void AddEntry(string name, int value, Address addr) =>
			_objects.UniquePush(new ObjectTable.Object {
				Name = name,
				Value = value,
				Address = addr
			});

		public static ObjectTable.Object GetObjectByName(string name) {
			var query = from obj in ObjectTable._objects
						where obj.Name == name
						select obj;
			if (query.Count() == 0) {
				return query.First<ObjectTable.Object>();
			}
			else {
				throw new ArgumentException($"{name} 对象未定义");
			}
		}

		public static ObjectTable.Object GetObjectByAddress(Address address) {
			var query = from obj in ObjectTable._objects
						where obj.Address == address
						select obj;
			if (query.Count() == 0) {
				return query.First<ObjectTable.Object>();
			}
			else {
				throw new ArgumentException($"对象未定义，地址: {ObjectTable.AddressEncode(address)}");
			}
		}
	}

	static class StackExtended {
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
