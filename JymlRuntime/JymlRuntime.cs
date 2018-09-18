using System;
using System.Collections.Generic;
using Arithmetic_Interpreter_UWP;

namespace JymlRuntime {
	public class Environment {
		private int _eid;
		private LinkedList<ObjectTable> _objectTables;
		public class EnvironmentNode {

		}

		public void AddTable(ObjectTable table) {

		}
	}

	/// <summary>
	/// 对象表
	/// 这里需要注意： address 是一个 proc，由 make-address 返回,
	/// 其带有一个参数和两个来自 make-address 的环境变量
	/// environment-id 在测试期间需要手动指定，当 object-table
	/// 真正添加到 environment 时，需要采取自动化措施填写该参数。
	/// </summary>
	public class ObjectTable {
		private int _environmentId;
		private static int _rowIndex = -1;
		private static Stack<ObjectTable.Object> _objects = null;

		public class Address {
			public int eid { get; set; }
			public int row { get; set; }
			public Address(int eid, int row) {
				this.eid = eid;
				this.row = row;
			}
		}

		public class Object {
			public Address address { get; set; }
			public string Name { get; set; }
			public int Value { get; set; }
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

		public static void AddEntry(string name,int value,Address addr) {
			_objects.Push(new ObjectTable.Object {
				Name = name,
				Value = value,
				address = addr
			});
		}

		public ObjectTable(int eid) {
			this._environmentId = eid;
		}


	}
}
