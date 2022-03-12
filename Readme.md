Net6.0

dotnet add package Newtonsoft.Json
dotnet add package Microsoft.AspNetCore.Session
dotnet add package Microsoft.Extensions.Caching.Memory
services.AddDistributedMemoryCache();           // Đăng ký dịch vụ lưu cache trong bộ nhớ (Session sẽ sử dụng nó)
services.AddSession(cfg => {                    // Đăng ký dịch vụ Session
    cfg.Cookie.Name = "xuanthulab";             // Đặt tên Session - tên này sử dụng ở Browser (Cookie)
    cfg.IdleTimeout = new TimeSpan(0,30, 0);    // Thời gian tồn tại của Session
});
Trong Startup.Configure cho thêm vào (sau UseStaticFiles()):

app.UseSession();