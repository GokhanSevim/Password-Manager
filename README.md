# Password Manager

Projede kullanılan teknolojiler:

Asp.Net Core MVC  
MSSQL  
MySQL  
PostgreSQL  
EntityFramework  
.Net Core SDK v7.0.203  
ASP.NET Core Identity  
UnitTest  
 
Bootstrap  
jQuery  
& Genel Kütüphaneler  
Progressive Web App (PWA)  
  
  
PM.UI katmanı içerisinde yer alan appsettings.json dosyasındaki ConnectionStrings bilgilerini kendi ortamınıza göre değiştirmelisiniz. Veri tabanı CodeFirst yöntemiyle geliştirilmiştir. MSSql, MySql ve PostgreSQL veri tabanı teknolojilerinden herhangi birini kullanabilirsiniz. Dilerseniz farklı veri tabanı teknolojilerini de mevcut yapıda değişiklik yapmadan ekleyebilirsiniz. Proje varsayılan veri tabanı teknolojisi olarak MSSql kullanılmaktadır. Değiştirmek istediğinizde PM.UI > appsettings.json > UserPreferences > PreferredDatabase parametresini düzenlemeniz ve uygulamayı restart etmeniz yeterli olacaktır. Veri tabanı geçiş isimlendirmesi şu şekildedir: MSSql kullanımı için "mssql", MySql kullanımı için "mysql", PostgreSql kullanımı için "postgresql" yazmanız yeterli olacaktır. Veri tabanlarının otomatik olarak oluşabilmesi için proje ayağa kalktığında ilk kullanıcı üyeliğini gerçekleştirmeniz yeterlidir. Hata almamanız için ConnectionStrings bilgilerinizi kontrol etmeyi unutmayın.

Projede iki farklı veri tabanı (DbContext) kullanılmıştır PassManager ve PassManagerIdentity. 
Site kullanımında kaydedilen Not ve Adres bilgileri haricindekiler için AES256 şifreleme yöntemi kullanılmıştır.
Secret Key bilgisi PassManagerIdentity veri tabanında ayrı olarak tutulmuştur. PassManager tablosunda kullanıcıların depoladığı bilgiler yer almaktadır.
Veri tabanlarının ayrı tutulması kullanıcı verilerinin çalınma ihtimalini zorlaştırmaktadır. Canlı ortamada Secret Manager Tool kullanılması ilave güvenlik sağlayacaktır.


Technologies used in the project:

Asp.Net Core MVC  
MSSQL  
MySQL  
PostgreSQL  
EntityFramework  
.Net Core SDK v7.0.203  
ASP.NET Core Identity  
  
UnitTest  
Bootstrap  
jQuery  
& General Libraries  
Progressive Web App (PWA)  
  
    
You should modify the ConnectionStrings information in the appsettings.json file located in the PM.UI layer according to your own environment. The database has been developed using the CodeFirst approach. You can use any of the MSSQL, MySQL, and PostgreSQL database technologies. You can also add different database technologies to the existing structure without making any changes. The default database technology used in the project is MSSQL. To change it, you just need to edit the PM.UI > appsettings.json > UserPreferences > PreferredDatabase parameter and restart the application. The naming for database transitions is as follows: for MSSQL usage, write "mssql"; for MySql usage, write "mysql"; for PostgreSql usage, write "postgresql". To automatically create the databases, all you need to do is perform the initial user registration when the project starts. Don't forget to check your ConnectionStrings information to avoid any errors.
  
Two different databases (DbContext) are used in the project: PassManager and PassManagerIdentity. AES256 encryption method is used for data other than Notes and Address information saved during site usage. The Secret Key information is kept separately in the PassManagerIdentity database. The PassManager table contains the information stored by users. Keeping the databases separate makes it more difficult for user data to be compromised. Using the Secret Manager Tool in a live environment will provide additional security.
