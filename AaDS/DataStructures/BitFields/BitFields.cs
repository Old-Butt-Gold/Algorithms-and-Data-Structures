namespace AaDS.DataStructures.BitFields;

static class BitFields
{
    //BitFields.Status test = BitFields.Status.Active | BitFields.Status.Editor;
    [Flags]
    public enum Status
    {    
        Active = 1 << 0,
        Admin = 1 << 1,
        Editor = 1 << 2,
        Moderator = 1 << 3
    }
}