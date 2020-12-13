using ClusterVR.CreatorKit;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Operation;


namespace Ponta.CCK_Generator.Base {

    public class LogicParamWrapper
    {

        public static SingleStatement SendSignalToSelf(Operator inOperator, string key, string sendKey) {

            var sendSignal = LogicParamGenerator.CreateSingleStatement(
                        LogicParamGenerator.CreateExpression_IF(inOperator, GimmickTarget.Item, key),
                        new Base.TargetState(TargetStateTarget.Item, sendKey, ParameterType.Signal));

            return sendSignal;
        }

    }

}

