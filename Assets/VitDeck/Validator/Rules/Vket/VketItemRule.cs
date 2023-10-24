using System.Linq;
using UnityEngine;
using VitDeck.Language;
using VRC.SDK3.Components;

namespace VitDeck.Validator
{
    /// <summary>
    /// ルートオブジェクト以外にPickupを付けないためのルール
    /// BoxCollider, Rigidbody, VRCPickupの有無を判別
    /// </summary>
    public class VketItemRule : BaseRule
    {
        public VketItemRule(string name) : base(name)
        {
        }

        protected override void Logic(ValidationTarget target)
        {
            var rootObjects = target.GetRootObjects();
            var targetObjects = target.GetAllObjects().ToList();
            // ルートオブジェクトをチェック対象から除外
            foreach (var rootObject in rootObjects)
            {
                targetObjects.Remove(rootObject);
            }
            
            // ルートオブジェクトのChildがチェック対象
            foreach (var targetObject in targetObjects)
            {
                LogicForItemRule(targetObject);
            }
        }

        void LogicForItemRule(GameObject targetObject)
        {
            if (targetObject.GetComponent<BoxCollider>())
            {
                AddIssue(new Issue(
                    targetObject,
                    IssueLevel.Error,
                    LocalizedMessage.Get("VketItemRule.UnauthorizedBoxCollider"),
                    LocalizedMessage.Get("VketItemRule.UnauthorizedBoxCollider.Solution")
                ));
            }
            
            if (targetObject.GetComponent<Rigidbody>())
            {
                AddIssue(new Issue(
                    targetObject,
                    IssueLevel.Error,
                    LocalizedMessage.Get("VketItemRule.UnauthorizedRigidbody"),
                    LocalizedMessage.Get("VketItemRule.UnauthorizedRigidbody.Solution")
                ));
            }
            
            if (targetObject.GetComponent<VRCPickup>())
            {
                AddIssue(new Issue(
                    targetObject,
                    IssueLevel.Error,
                    LocalizedMessage.Get("VketItemRule.UnauthorizedVRCPickup"),
                    LocalizedMessage.Get("VketItemRule.UnauthorizedVRCPickup.Solution")
                ));
            }
        }
    }
}
