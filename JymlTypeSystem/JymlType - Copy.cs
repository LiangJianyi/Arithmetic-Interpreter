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
        private string Identity { get; set; }
        private List<JymlType> Args { get; set; }
        private EnvironmentNode Body { get; set; }
        public override string ToString() {
            throw new NotImplementedException();
        }
    }
}
