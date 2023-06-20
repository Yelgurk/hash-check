using EnumFastToStringGenerated;
// ReSharper disable InconsistentNaming

namespace HashCheck.Domain;

[EnumGenerator]
public enum HashAlgorithmType
{
    MD5,
    SHA1,
    SHA256,
    SHA384,
    SHA512,
}