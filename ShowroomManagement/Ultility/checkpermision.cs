using ShowroomManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShowroomManagement.Ultility
{
    public class checkpermision
    {        
        public static bool CheckPermission(int userid, int objectid)
        {
            var db = new showroomEntities();
            var isPermis = (from u in db.users
                            join g in db.groups on u.group_id equals g.group_id
                            join go in db.group_objects on g.group_id equals go.group_id
                            join o in db.objects1 on go.object_id equals o.object_id
                            where u.user_id == userid && o.object_id == objectid
                            select o).FirstOrDefault(); 

            return isPermis != null;
        }
    }
}