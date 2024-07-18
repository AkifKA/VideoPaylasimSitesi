<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sil.aspx.cs" Inherits="VideoPaylasimSitesi.AdminPanel.Sil" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sil</title>
    <style>
        .countdown {
            font-size: 2rem;
        }
        .text-center {
            text-align: center;
            margin-top: 20px;
        }
        .fs-2 {
            font-size: 2rem;
        }
        .text-success {
            color: green;
        }
        .text-danger {
            color: red;
        }
    </style>
    <script type="text/javascript">
        function startCountdown(url, delay) {
            var countdownElement = document.getElementById("countdown");
            var count = delay / 1000;
            countdownElement.innerText = count;
            countdownElement.style.display = "block";

            var interval = setInterval(function () {
                count--;
                countdownElement.innerText = count;
                if (count <= 0) {
                    clearInterval(interval);
                    window.location.href = url;
                }
            }, 1000);
        }
    </script>
</head>
<body>
                     <% 
        if (Session["yoneticiYetki"] == null)
       {
   %>
   <div class="alert alert-danger text-center w-50 mx-auto" role="alert">
       Silme işlemi için erişim yetkiniz bulunmamaktadır.
       </div>
      <div class="text-center">
      <asp:HyperLink CssClass="btn btn-primary" NavigateUrl="~/AdminPanel/Giris.aspx" Text="Giriş Yap" runat="server" />
  </div>
           <% }
           else
           { %>
    <form id="form1" runat="server">
        <div class="text-center fs-2 text-success">
            <asp:Literal ID="SuccessMessage" runat="server" Visible="false" />
        </div>
        <div class="text-center fs-2 text-danger">
            <asp:Literal ID="ErrorMessage" runat="server" Visible="false" />
        </div>
        <div class="text-center countdown text-success" id="countdown" style="display: none;"></div>
    </form>
        <% } %>
</body>
</html>
