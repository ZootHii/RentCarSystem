using System.Linq;
using Core.Utilities.Results.Abstract;

namespace Core.Utilities.Business
{
    public static class BusinessRules
    {
        public static IResult Run(params IResult[] rules)
        {
            return rules.FirstOrDefault(rule => !rule.Success);
        }

        public static IDataResult<T> Run<T>(params IDataResult<T>[] rules) // generic method without non generic class
        {
            IDataResult<T> dataResult = null;
            foreach (var rule in rules)
            {
                dataResult = rule;
            }

            return dataResult;
        }
    }
}