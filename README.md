## BTTH4 

Lê Hoàng Nam -2221050640

```csharp
## 1. Tìm hiểu về ViewBag trong MVC

- **ViewBag** là một đối tượng dùng để **truyền dữ liệu từ Controller sang View** trong ASP.NET MVC.
- Dữ liệu trong ViewBag có kiểu **dynamic**, có thể truy cập trực tiếp trong View.

**Ví dụ:**
// Controller
public ActionResult Index()
{
    ViewBag.Message = "Hello từ Controller";
    return View();
}

// View (Index.cshtml)
<h2>@ViewBag.Message</h2>
Kết quả: Hiển thị “Hello từ Controller” trên trang web.

2. Gửi nhận dữ liệu giữa View và Controller qua Form Submit
Dữ liệu từ người dùng nhập trên View có thể gửi lên Controller bằng form.

Ví dụ: nhập Họ tên và hiển thị thông báo chào.

View (Index.cshtml):

html
Sao chép mã
<form method="post" action="/Student/Greet">
    <input type="text" name="fullName" placeholder="Nhập họ tên" />
    <button type="submit">Gửi</button>
</form>

@if(ViewBag.Greeting != null)
{
    <p>@ViewBag.Greeting</p>
}
Controller (StudentController.cs):

csharp
Sao chép mã
[HttpPost]
public ActionResult Greet(string fullName)
{
    ViewBag.Greeting = "Xin chào " + fullName;
    return View("Index");
}
Khi người dùng nhập tên và bấm “Gửi”, Controller nhận dữ liệu, xử lý và gửi thông báo về View.

3. Tìm hiểu về Models
Model là nơi định nghĩa dữ liệu và cấu trúc dữ liệu trong MVC.

Ví dụ tạo class Student:

csharp
Sao chép mã
public class Student
{
    public string StudentCode { get; set; }
    public string FullName { get; set; }
}
Model giúp quản lý dữ liệu có cấu trúc và dễ dàng gửi nhận giữa Controller và View.

4. Gửi nhận dữ liệu kiểu Student
Controller:

csharp
Sao chép mã
public ActionResult CreateStudent()
{
    return View();
}

[HttpPost]
public ActionResult CreateStudent(Student student)
{
    ViewBag.Message = "Sinh viên " + student.FullName + " đã được thêm.";
    return View();
}
View (CreateStudent.cshtml):

html
Sao chép mã
<form method="post" asp-action="CreateStudent">
    <input type="text" name="StudentCode" placeholder="Mã sinh viên" />
    <input type="text" name="FullName" placeholder="Họ tên" />
    <button type="submit">Thêm sinh viên</button>
</form>

@if(ViewBag.Message != null)
{
    <p>@ViewBag.Message</p>
}
Khi submit form, dữ liệu từ View được tự động binding vào đối tượng Student trong Controller.

5. Tìm hiểu về Layout và điều hướng
Layout giúp tạo giao diện chung cho tất cả các View (header, footer, menu…)

Ví dụ thêm liên kết tới StudentController:

html
Sao chép mã
<nav>
    <a href="/Student/CreateStudent">Thêm Sinh viên</a>
    <a href="/Student/Index">Danh sách Sinh viên</a>
</nav>
Mọi View có thể sử dụng Layout chung để đồng bộ giao diện.
```
