using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidation
{
    public class Rule<T>
    {
        //===============================================================
        public Rule(Func<T, bool> rule, Func<T, string> failureMsg)
        {
            Evaluator = rule;
            FailureMessage = failureMsg;
        }
        //===============================================================
        public virtual String Evaluate(T val, Func<T, string> customFailureMsg = null)
        {
            if (!Evaluator(val))
                return customFailureMsg != null ? customFailureMsg(val) : FailureMessage(val);

            return null;
        }
        //===============================================================
        public Func<T, bool> Evaluator { get; private set; }
        //===============================================================
        public Func<T, string> FailureMessage { get; private set; }
        //===============================================================
    }
}
