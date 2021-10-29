Veri tabanı yedeğini restore ettikten sonra Sql server bağlantı arayüzündeki "Server Name" ile
Projedeki Web.config dosyası altında bulunan "data source" kısmını değitiriniz.

<connectionStrings>
    <add name="BitirmeProjesiDBEntities" connectionString="metadata=res://*/Models.Entity.Model1.csdl|res://*/Models.Entity.Model1.ssdl|res://*/Models.Entity.Model1.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=DESKTOP-J0PNPO8;initial catalog=BitirmeProjesiDB;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
  </connectionStrings>

admin paneli	:	localhost:xxxx/admin
rol ekleme	:	localhost:xxxx/roleadmin/create
kullanıcı engelleme/rol güncelleme :	localhost:xxxx/roleadmin