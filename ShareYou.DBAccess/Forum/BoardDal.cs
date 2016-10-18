using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareYou.IDBAccess.Forum;
using ShareYou.Model.Forum;

namespace ShareYou.DBAccess.Forum
{
    public class BoardDal:IBoardDal
    {
        public ForumBoard GetBoard(int boardid)
        {
            string sql = "select * from forum_board where boardid=@boardid";
            ForumBoard board = null;
            SqlParameter spm = new SqlParameter("@boardid", SqlDbType.Int) {Value = boardid};
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql, CommandType.Text, spm))
            {
                if (reader.HasRows)
                {
                    board=new ForumBoard();
                    reader.Read();
                    board.BoardId = reader.GetInt32(0);
                    board.Title = reader.GetString(1);
                    board.Description = reader.GetString(2);
                    board.DateLine = reader.GetDateTime(3);
                }
            }
            return board;
        }

        public IList<ForumBoard> GetBoards()
        {
            string sql = "select * from forum_board";
            List<ForumBoard> boards=new List<ForumBoard>();
            DataSet dataSet=new DataSet();
            SqlHelper.GetDataTable(sql, CommandType.Text, dataSet);
            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                ForumBoard board=new ForumBoard();
                board.BoardId = Convert.ToInt32(dataRow[0]);
                board.Title = dataRow[1].ToString();
                board.Description = dataRow[2].ToString();
                board.DateLine = Convert.ToDateTime(dataRow[3]);
                boards.Add(board);
            }
            return boards;
        }
    }
}
