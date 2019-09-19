using System;
using System.Collections.Generic;

namespace JymlTypeSystem {
    public abstract class JymlType {
        public abstract string ToString();
    }

    public class Number : JymlType {
        public override string ToString() {
            throw new NotImplementedException();
        }
    }

    public class String : JymlType {
        public override string ToString() {
            throw new NotImplementedException();
        }
    }

    public class DateTime : JymlType {
        public override string ToString() {
            throw new NotImplementedException();
        }
    }

    public class Procedure : JymlType {
        /// <summary>
        /// 过程标识符
        /// </summary>
        public string Identity { get; set; }
        /// <summary>
        /// 参数列表
        /// </summary>
        public List<JymlType> Args { get; set; }
        /// <summary>
        /// 过程体用运行时的环境节点表示
        /// </summary>
        public JymlRuntime.Environment.EnvironmentNode Body { get; set; }

        public override string ToString() => $"#<JymlType:Procedure:{Identity}>";
    }
}
