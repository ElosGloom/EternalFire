namespace Game.Scripts.Fire
{
    public class Torch : FireSystemMember
    {
        private void Start()
        {
            FireSystem.Instance.ConnectNewTorch(this);
        }
    }
}