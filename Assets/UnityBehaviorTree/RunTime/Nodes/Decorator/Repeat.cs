namespace UnityBehaviorTree.Core.Decorator
{
    public class Repeat : DecoratorNode
    {
        private int _numCycles = 0;

        private int _currentCycle = 0;

        public Repeat() { }
        
        public Repeat(BTNode child, int numCycles) : base(child)
        {
            _numCycles = numCycles;
        }
        
        protected override void OnStart()
        {
            _currentCycle = 0;
        }

        protected override void OnStop()
        {
        }

        protected override EStatus OnUpdate()
        {
            for (int i = _currentCycle; i < _numCycles; i++)
            {
                _currentCycle = i;
                Child.Tick();
                switch (Child.Status)
                {
                    case EStatus.Success:
                        continue;
                    case EStatus.Running:
                        return _status = EStatus.Running;
                    case EStatus.Failed:
                        return _status = EStatus.Failed;
                    default:
                        return _status = EStatus.Failed;
                }
            }

            return _status = EStatus.Success;
        }
    }
}