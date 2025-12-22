namespace MoraJai.Lib.Tests;

public class VerifyInitializer
{
    [Before(TestSession)]
    public static void Initialize()
    {
        Verifier.UseProjectRelativeDirectory("Snapshots");
    }
}
