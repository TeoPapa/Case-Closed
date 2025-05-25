using UnityEngine;

public class HiddenCaseValue : CaseItemType {
    CaseItemType Value;

    public HiddenCaseValue(Sprite c, string n, string d, bool ii, CaseItemType o) : base(c, n, d, ii) {
        Value = o;
    }
}
