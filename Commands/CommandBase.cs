namespace Pkgscan.Commands
{
    public abstract class CommandBase
    {
        public abstract void RunCommand<TOptions>(TOptions options, string path);
    }
}