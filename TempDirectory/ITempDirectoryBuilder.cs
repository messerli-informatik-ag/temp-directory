namespace Messerli.TempDirectory
{
    public delegate TempDirectory TempDirectoryFactory;

    public interface ITempDirectoryBuilder
    {
        ITempDirectoryBuilder Prefix(string prefix);

        ITempDirectoryBuilder PrefixSeparator(string prefixSeparator);

        ITempDirectoryBuilder Suffix(string suffix);

        ITempDirectoryBuilder SuffixSeparator(string suffixSeparator);

        TempDirectory Create();
    }
}
