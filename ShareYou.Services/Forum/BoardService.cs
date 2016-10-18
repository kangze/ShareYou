using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.IDBSession;
using ShareYou.IServices.Forum;
using ShareYou.Model.CustomeException;
using ShareYou.Model.Forum;

namespace ShareYou.Services.Forum
{
    public class BoardService:IBoardService
    {
        public IDbSession DbSession { get; set; }
        public ForumBoard GetBoard(int boardid)
        {
            ForumBoard board = DbSession.BoardDal.GetBoard(boardid);
            if(null==board)
                throw new UserException("版块不存在");
            return board;
        }

        public IList<ForumBoard> GetBoards()
        {
            return DbSession.BoardDal.GetBoards();
        }
    }
}
