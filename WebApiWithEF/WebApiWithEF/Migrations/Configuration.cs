namespace WebApiWithEF.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApiWithEF.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApiWithEF.DAL.POIContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApiWithEF.DAL.POIContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var pois = new List<POI>(){
            new POI {Name = "����������ʷ���й�",C_ID=1,D_Name="����ͼ��",Address="����������ͼ������·32��",Time="8:00-17:00",Tele="0459-5813777",Abstract="����������ʷ���й�ռ�����15900��ƽ���ף���չ���4200��ƽ����",Ticket="5Ԫ",Status = 1, Created = DateTime.Now, Updated= DateTime.Now},
            new POI {Name = "��������ά��Ƶ�",C_ID=2,D_Name="����ͼ��",Address="�����и������·�·9��",Tele="0459-8166666",Abstract="��������ά��Ƶ��Ǵ���Ψһ���Ǽ����ʱ�׼�ۺϾƵ꣬�ش����¼���������",Ticket="1958Ԫ�����������׷���",Type="���Ǽ�",Status =1,Created = DateTime.Now, Updated= DateTime.Now},
            new POI {Name = "һ�����´�꣩",C_ID=3,D_Name="����ͼ��",Address="����������ͼ��γ��·180��",Time="9:00-22:00",Tele="0459-4621801",Abstract="��˾������1995�꣬��˾ӵ�й���רҵƷ�ơ�һ������ȫ��֪ʶ��Ȩ��",Ticket="60Ԫ", Status=1,Created = DateTime.Now, Updated= DateTime.Now},
            new POI {Name = "�ֶ��꾭���ֵ�",C_ID=4,D_Name="����ͼ��",Address="����������ͼ��������60��",Time="8:00-22:00",Tele="0459-8992888",Abstract="��һ���ֶ��깺��㳡��1988��3��1���������������ݵĻ�ʢ�ٿ�ҵ��", Status=1,Created = DateTime.Now, Updated= DateTime.Now}
            };

            pois.ForEach(p => context.POIs.AddOrUpdate(c => c.Name, p));
            context.SaveChanges();
        }
    }
}
