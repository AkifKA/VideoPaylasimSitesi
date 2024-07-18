<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Giris.aspx.cs" Inherits="VideoPaylasimSitesi.UserInterface.Giris" %>

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
         <link rel="icon" type="image/png" href="Resimler/f.png" />
   

</head>
<body>
<div class="d-flex justify-content-center align-items-center vh-100 bg-light">
        <form runat="server" class="d-flex flex-column justify-content-center align-items-center p-5 bg-secondary shadow rounded text-light">
            <h4 class="text-center mb-2">MAK Video Paylaşım Sitesi</h4>
            <h5 class="text-center mb-2">Kullanıcı Giriş Formu</h5>
            <div class="d-flex justify-content-center"><i class="fas fa-lock fs-1"></i> </div>
            <div class="form-group w-100 mt-2">
                <asp:Label ID="LblTakmaAd" CssClass="mt-1" AssociatedControlID="TxtTakmaAd" Text="Takma Adınızı Giriniz" runat="server"></asp:Label>
                <asp:TextBox ID="TxtTakmaAd" CssClass="form-control mt-1" Placeholder="Takma Ad" runat="server"></asp:TextBox>
            </div>
            <div class="form-group w-100 mt-2">
                <asp:Label ID="LblSifre" CssClass="mt-1" AssociatedControlID="TxtSifre" Text="Şifrenizi Giriniz" runat="server"></asp:Label>
                <asp:TextBox ID="TxtSifre" CssClass="form-control mt-1" TextMode="Password" Placeholder="Şifre" runat="server"></asp:TextBox>
            </div>
            <div >
                <asp:Label ID="LblHataMesaji" CssClass="text-danger mt-2" runat="server"></asp:Label></div>
            <asp:Button ID="BtnGirisYap" CssClass="btn btn-primary w-100 mt-4" Text="Giriş Yap" runat="server" OnClick="BtnGirisYap_Click" />
               <p class="text-center mt-2">Hesabınız yok mu? <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Kayit.aspx">Buradan</asp:HyperLink> sayfamıza kaydolabilirsiniz.</p>
             <asp:HyperLink ID="UserInterFaceLink" runat="server" CssClass="link mt-1" NavigateUrl="index.aspx">Ana Sayfa</asp:HyperLink>
        </form>
   
    </div>
    <!-- Bootstrap 5.3.3 JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</body>
</html>
