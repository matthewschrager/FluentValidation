using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidation
{
    public static class Validate
    {
        //===============================================================
        public static ValidationContext<T> For<T>(T val)
        {
            return new ValidationContext<T>(val);
        }
        //===============================================================
    }

    public class ValidationContext<T>
    {
        //===============================================================
        public ValidationContext(T value)
        {
            Value = value;
            ValidationErrors = new List<string>();
        }
        //===============================================================
        private T Value { get; set; }
        //===============================================================
        private IList<String> ValidationErrors { get; set; }
        //===============================================================
        public bool HasErrors
        {
            get { return ValidationErrors.Any(); }
        }
        //===============================================================
        private void EvaluateRules<TValue>(TValue val, IEnumerable<Rule<TValue>> rules)
        {
            foreach (var rule in rules)
            {
                var error = rule.Evaluate(val);
                if (error != null)
                    ValidationErrors.Add(error);
            }
        }
        //===============================================================
        public ValidationContext<T> That(Func<T, bool> rule, Func<T, string> errorMsg)
        {
            return That(new Rule<T>(rule, errorMsg));
        }
        //===============================================================
        public ValidationContext<T> That(Func<T, bool> rule, String errorMsg)
        {
            return That(rule, x => errorMsg);
        }
        //===============================================================
        public ValidationContext<T> That(params Rule<T>[] rules)
        {
            return That((IEnumerable<Rule<T>>)rules);
        }
        //===============================================================
        public ValidationContext<T> That(IEnumerable<Rule<T>> rules)
        {
            EvaluateRules(Value, rules);
            return this;
        }
        //===============================================================
        public ValidationContext<T> That<TProperty>(Func<T, TProperty> propertySelector, params Rule<TProperty>[] rules)
        {
            return That(propertySelector, (IEnumerable<Rule<TProperty>>)rules);
        }
        //===============================================================
        public ValidationContext<T> That<TProperty>(Func<T, TProperty> propertySelector, IEnumerable<Rule<TProperty>> rules)
        {
            EvaluateRules(propertySelector(Value), rules);
            return this;
        }
        //===============================================================
        public ValidationContext<T> That<TProperty>(Rule<TProperty> rule, params Func<T, TProperty>[] propertySelectors)
        {
            return That(rule, (IEnumerable<Func<T, TProperty>>)propertySelectors);
        }
        //===============================================================
        public ValidationContext<T> That<TProperty>(Rule<TProperty> rule, IEnumerable<Func<T, TProperty>> propertySelectors)
        {
            foreach (var selector in propertySelectors)
                That(selector, rule);

            return this;
        }
        //===============================================================
        public IEnumerable<String> Errors
        {
            get { return ValidationErrors; }
        }
        //===============================================================
    }
}
