using UnityEngine;

[CreateAssetMenu(fileName = "New BlockBundleData", menuName = "Block Bundle Data", order = 1)]
public class BlockBundleData : ScriptableObject
{
   [SerializeField] 
   private BlockData[] blockData;

   public BlockData[] BlockData 
   { 
        get 
        {
            return blockData;
        } 
   }
}
