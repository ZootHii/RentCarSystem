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
    }
}