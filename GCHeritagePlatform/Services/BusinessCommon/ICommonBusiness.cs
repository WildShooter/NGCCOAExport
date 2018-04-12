namespace GCHeritagePlatform.Services
{
    interface ICommonBusiness 
    {

        string SelectByWhere(string cols, string where, string orderBy, int pageSize, int pageIndex, bool returnSum);
        string GetDetailByMainID( string mainId);

    }



}
