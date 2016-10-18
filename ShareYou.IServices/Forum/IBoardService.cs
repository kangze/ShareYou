using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.Model.Forum;

namespace ShareYou.IServices.Forum
{
    public interface IBoardService
    {
        ForumBoard GetBoard(int boardid);

        IList<ForumBoard> GetBoards();
    }
}
