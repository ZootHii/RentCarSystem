using System.Linq;
using Core.Utilities.Results.Abstract;

namespace Core.Utilities.Business
{
    public static class BusinessRules
    {
        public static IResult Run(params IResult[] rules)
        {
            foreach (var rule in rules)
            {
                if (!rule.Success) return rule;
            }

            return null;
        }

        public static IDataResult<T> Run<T>(params IDataResult<T>[] rules) // generic method without non generic class
        {
            foreach (var rule in rules)
            {
                if (!rule.Success) return rule;
            }

            return null;
        }
    }
}