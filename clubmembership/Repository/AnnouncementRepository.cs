using clubmembership.Data;
using clubmembership.Models;
using clubmembership.Models.DBObjects;
using System.Security.Cryptography.X509Certificates;

namespace clubmembership.Repository
{
    public class AnnouncementRepository
    {
        private readonly ApplicationDbContext _DbContext;

        public AnnouncementRepository()
        { 
            _DbContext= new ApplicationDbContext();
        }

        public AnnouncementRepository(ApplicationDbContext dbContext)
        {
            _DbContext = dbContext;
        }

        private AnnouncementModel MapDBObjectToModel(Announcemment dbObject)
        {
            var model = new AnnouncementModel();
            if (dbObject != null)
            { 
                model.Idannouncemment=dbObject.Idannouncemment;
                model.ValidFrom=dbObject.ValidFrom;
                model.ValidTo=dbObject.ValidTo;
                model.Title = dbObject.Title;
                model.Text = dbObject.Text;
                model.EventDateTime = dbObject.EventDateTime;
                model.Tags = dbObject.Tags;
            }
            return model;
        }

        private Announcemment MapModelToDBObject(AnnouncementModel model)
        { 
            var dbObject= new Announcemment();
            if (model != null)
            {
                dbObject.Idannouncemment = model.Idannouncemment;
                dbObject.ValidFrom = model.ValidFrom;
                dbObject.ValidTo = model.ValidTo;
                dbObject.Text = model.Text;
                dbObject.EventDateTime = model.EventDateTime;
                dbObject.Tags = model.Tags;
                dbObject.Title = model.Title;
            }
                
            return dbObject;
        }

        public List<AnnouncementModel> GetAllAnnouncements()
        { 
            var list = new List<AnnouncementModel>();
            foreach (var dbobject in _DbContext.Announcemments)
            {
                list.Add(MapDBObjectToModel(dbobject));
            }
            return list;
        }

        public AnnouncementModel GetAnnouncementbyID(Guid id)
        {
            return MapDBObjectToModel(_DbContext.Announcemments.FirstOrDefault(x => x.Idannouncemment == id));
        }

        public void InsertAnnouncement(AnnouncementModel model)
        {
            model.Idannouncemment = Guid.NewGuid();
            _DbContext.Announcemments.Add(MapModelToDBObject(model));
            _DbContext.SaveChanges();
        }

         public void UpdateAnnoucement(AnnouncementModel model)
            {
                var dbObject = _DbContext.Announcemments.FirstOrDefault(x => x.Idannouncemment == model.Idannouncemment);
                if (dbObject != null)
                {
                    dbObject.Idannouncemment = model.Idannouncemment;
                    dbObject.ValidFrom = model.ValidFrom;
                    dbObject.ValidTo = model.ValidTo;
                    dbObject.Text = model.Text;
                    dbObject.EventDateTime = model.EventDateTime;
                    dbObject.Tags = model.Tags;
                    dbObject.Title = model.Title;
                    _DbContext.SaveChanges();
                }
            }

         public void DeleteAnnouncement(AnnouncementModel model)
            {
                var dbObject = _DbContext.Announcemments.FirstOrDefault(x => x.Idannouncemment == model.Idannouncemment);
                if (dbObject != null)
                {
                    _DbContext.Announcemments.Remove(dbObject);
                    _DbContext.SaveChanges();
                }
            }
    }
}
