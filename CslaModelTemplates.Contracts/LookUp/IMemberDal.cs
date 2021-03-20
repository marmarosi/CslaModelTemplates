namespace CslaModelTemplates.Contracts.LookUp
{
    /// <summary>
    /// Defines the data access functions of the editable member object.
    /// </summary>
    public interface IMemberDal
    {
        void Insert(MemberDao dao);
        void Delete(MemberDao dao);
    }
}
