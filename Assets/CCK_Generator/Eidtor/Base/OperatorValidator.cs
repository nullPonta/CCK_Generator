#if UNITY_EDITOR
using ClusterVR.CreatorKit.Operation;
using UnityEngine;


namespace Ponta.CCK_Generator.Base
{

    public static class OperatorValidator
    {

        public static bool Validate_IsCompareOperator(Operator ope) {

            var result = IsCompareOperator(ope);

            if (!result) {
                Debug.LogError("Input is not compare operator!");
            }

            return result;
        }

        public static bool Validate_IsCalculateOperator(Operator ope) {

            var result = IsCalculateOperator(ope);

            if (!result) {
                Debug.LogError("Input is not calculate operator!");
            }

            return result;
        }

        public static bool IsCompareOperator(Operator ope) {

            if ((ope == Operator.Equals) ||
                (ope == Operator.NotEquals) ||
                (ope == Operator.GreaterThan) ||
                (ope == Operator.GreaterThanOrEqual) ||
                (ope == Operator.LessThan) ||
                (ope == Operator.LessThanOrEqual) ||
                (ope == Operator.And) ||
                (ope == Operator.Or)) {

                return true;
            }

            return false;
        }

        public static bool IsCalculateOperator(Operator ope) {

            if ((ope == Operator.Add) ||
                (ope == Operator.Multiply) ||
                (ope == Operator.Subtract) ||
                (ope == Operator.Divide) ||
                (ope == Operator.Modulo) ||
                (ope == Operator.Min) ||
                (ope == Operator.Max)) {

                return true;
            }

            return false;
        }

    }

}
#endif
