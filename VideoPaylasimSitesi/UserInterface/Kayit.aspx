<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Kayit.aspx.cs" Inherits="VideoPaylasimSitesi.UserInterface.Kayit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<!DOCTYPE html>
<html lang="en">
<head>
    <title>Kullanıcı Giriş Sayfası</title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- Bootstrap 5.3.3 CSS CDN -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">

    <%--FontAwesome 5.15.4 CSS CDN--%>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.4/css/all.min.css" rel="stylesheet">

    <link rel="icon" type="image/x-icon" href="Resimler/f.ico" />
</head>
<body>
    <div class="d-flex justify-content-center align-items-center vh-100 bg-light">
        <form runat="server" class="d-flex flex-column justify-content-center align-items-center p-5 bg-secondary shadow rounded text-light">
            <h4 class="text-center mb-2">MAK Video Paylaşım Sitesi</h4>
            <h5 class="text-center mb-2">Kullanıcı Kayıt Formu</h5>
            <div class="d-flex justify-content-center"><i class="fas fa-lock fs-1"></i></div>
            <div class="form-group w-100 mt-2">
                <asp:Label ID="LblAd" CssClass="mt-1" AssociatedControlID="TxtAd" Text="Ad" runat="server"></asp:Label>
                <asp:TextBox ID="TxtAd" CssClass="form-control mt-1" Placeholder="Adınızı Giriniz" runat="server"></asp:TextBox>
            </div>
            <div class="form-group w-100 mt-2">
                <asp:Label ID="LblSoyad" CssClass="mt-1" AssociatedControlID="TxtSoyad" Text="Soyad" runat="server"></asp:Label>
                <asp:TextBox ID="TxtSoyad" CssClass="form-control mt-1" Placeholder="Soyadınızı Giriniz" runat="server"></asp:TextBox>
            </div>
            <div class="form-group w-100 mt-2">
                <asp:Label ID="LblTakmaAd" CssClass="mt-1" AssociatedControlID="TxtTakmaAd" Text="Takma Ad" runat="server"></asp:Label>
                <asp:TextBox ID="TxtTakmaAd" CssClass="form-control mt-1" Placeholder="Takma Adınızı Giriniz" runat="server"></asp:TextBox>
            </div>
            <div class="form-group w-100 mt-2">
                <asp:Label ID="LblEmail" CssClass="mt-1" AssociatedControlID="TxtMail" Text="Email" runat="server"></asp:Label>
                <asp:TextBox ID="TxtMail" CssClass="form-control mt-1" Placeholder="Mail Adresinizi Giriniz" runat="server"></asp:TextBox>
            </div>
            <div class="form-group w-100 mt-2">
                <asp:Label ID="LblSifre" CssClass="mt-1" AssociatedControlID="TxtSifre" Text="Şifrenizi Giriniz" runat="server"></asp:Label>
                <asp:TextBox ID="TxtSifre" CssClass="form-control mt-1" TextMode="Password" Placeholder="Şifre" runat="server"></asp:TextBox>
            </div>
            <div class="form-group w-100 mt-2">
                <asp:Label ID="LblResim" CssClass="mt-1" AssociatedControlID="fileUpload" Text="Profil Resmi Yükleyiniz" runat="server"></asp:Label>
                <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control mt-1" />
            </div>
            <asp:Button ID="BtnKayitYap" CssClass="btn btn-primary w-100 mt-4 mb-2" Text="Kaydol" runat="server" OnClick="BtnKayitYap_Click" />
            <p>Hesabınız var mı?
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Giris.aspx">Buradan</asp:HyperLink>
                sayfamıza giriş yapabilrsiniz.</p>
            <div>
                <asp:Label ID="LblResHataMesaji" CssClass="text-danger mt-2" runat="server"></asp:Label>
                <asp:Label ID="LblHataMesaji" CssClass="text-danger mt-2" runat="server"></asp:Label>
            </div>
        </form>
    </div>
    <!-- Bootstrap 5.3.3 JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</body>
</html>
