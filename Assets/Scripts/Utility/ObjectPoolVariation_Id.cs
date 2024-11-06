using UnityEngine;
using VInspector;

public class ObjectPoolVariation_Id : MonoBehaviour
{
    [SerializeField]
    [ReadOnly]
    private int variationId;

    public int VariationId => variationId;

    public void Init(int variationId)
    {
        this.variationId = variationId;
    }
}
