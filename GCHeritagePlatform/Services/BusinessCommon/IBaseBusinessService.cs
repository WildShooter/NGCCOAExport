using System;

namespace GCHeritagePlatform.Services
{

    interface IBaseBusinessService
    {
        bool DoCheck(Guid id);
        bool Sumbit(Guid id);
    }
}
