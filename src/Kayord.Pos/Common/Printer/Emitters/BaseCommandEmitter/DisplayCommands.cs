using Kayord.Pos.Common.Printer.Emitters.BaseCommandValues;

namespace Kayord.Pos.Common.Printer.Emitters;

public abstract partial class BaseCommandEmitter : ICommandEmitter
{
    /* Display Commands */
    public virtual byte[] Clear() => new byte[] { Display.CLR };
}

