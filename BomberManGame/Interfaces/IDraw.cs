using System;
namespace BomberManGame
{
    public interface IDraw
    {
        public void onDraw()
        {
            throw new NotImplementedException();
        }

        public void IDrawSubscribe()
        {
            EventPublisher.Instance.GetEvent<EDraw>().Subscribe(this);
        }
    }
}
