# Single Sign On - ASP NET MVC
In order to secure our web page, we need to provide authentication. If user would like to access multiple web, they need to provide multiple login. So, to simplify the login process in single time we able to use SSO

## SSO - Scenario
We have 2 web application. The first is "SSONetFramework", the other one is "SSONetFramework2". If we have logged in for one of both application, then we could access the other web application

### Step by Step

1. The first step is we will share cookie between the applications, by using this code below. It means all application will use this setting
2. Machine key will be placed within system.web
3. Machine key can be generated from IIS (https://www.codeproject.com/Articles/221889/How-to-Generate-Machine-Key-in-IIS7). If Machine key doesn't appear in IIS yet, refer to (https://serverfault.com/questions/554788/missing-machine-key-in-iis-manager)

```
<machineKey validationKey="A638A93ED8222EDC9855E2595575061F93153A63775D2A4648EE2F0B051655097DDBA1CC6D8B580A268A07979E00D1F6DF8EAE3D0FA99229400EFACB6D80DD62"
					decryptionKey="E6233D8DA0702F5AA6086516A08711D75EB0316E7B14B99A" validation="HMACSHA256" decryption="AES" />
```

4. In order to simplify the authentication process, we will use using from authentication. We will pass user & password : demo. In real application we will replace this authentication mode with the database
5. Paste this code below the machine key in web.config

```
<authentication mode="Forms">
  <forms name="SingleSignOn" loginUrl="https://localhost:44376/Account/Login" timeout="480" slidingExpiration="true">
    <credentials passwordFormat="SHA1">
		  <user name="demo" password="89e495e7941cf9e40e6980d14a16bf023ccd4c91"/>
			<!--password = demo-->
		</credentials>
  </forms>
</authentication>
```

6. Account Controller - Login Get
- returnUrl , by default will have path URL. This is convention in ASP.NET, If you change the parameter name it won't works

```
public ActionResult Login(string returnUrl)
{
    if (Request.IsAuthenticated)
    {
        return RedirectToAction("Index", "Home");
    }

    ViewBag.ReturnUrl = returnUrl;
    return View();
}
```

7. Acount Controller - Login Post
- isAuth  variable can be replaced by database authentication. Form authontication just simplify the process

```
public ActionResult Login(string username, string password, string returnUrl)
{
    bool isAuth = FormsAuthentication.Authenticate(username, password);

    if (isAuth)
    {
        FormsAuthentication.SetAuthCookie(username, false);
        if (!string.IsNullOrEmpty(returnUrl))
        {
            return Redirect(returnUrl);
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }
     }
     else
     {
        ModelState.AddModelError(string.Empty, "Invalid login details");
        ViewBag.ReturnUrl = returnUrl;
        return View();
     }
}
```

8. Account Controller - Logout

```
public ActionResult Logout()
{
    FormsAuthentication.SignOut();
    return RedirectToAction("Login");
}
 ```
 
 REFERENCE : https://arunendapally.com/post/implementation-of-single-sign-on-(sso)-in-asp.net-mvc
