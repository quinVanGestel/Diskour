interface IKillable
{
    public abstract void Kill();
}

interface ISpikeAble : IKillable
{
}

interface IStayInBounds : IKillable
{
}
