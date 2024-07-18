<%@ Page Title="Yorumlar" Language="C#" MasterPageFile="~/AdminPanel/Admin.Master" AutoEventWireup="true" CodeBehind="Yorumlar.aspx.cs" Inherits="VideoPaylasimSitesi.AdminPanel.Yorumlar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <h2 class="text-center">Yorumlar</h2>
        <% 
            if (Session["yoneticiYetki"] == null)
            {%>
        <div class="alert alert-danger text-center w-50 mx-auto" role="alert">
            Bu sayfaya erişim yetkiniz bulunmamaktadır. Lütfen yönetici girişi yapınız.
        </div>
        <div class="text-center">
            <asp:HyperLink CssClass="btn btn-primary" NavigateUrl="~/AdminPanel/Giris.aspx" Text="Giriş Yap" runat="server" />
        </div>
        <% 
            }
            else if (Session["yoneticiYetki"].ToString() != "Süper Yönetici" && Session["yoneticiYetki"].ToString() != "Yorum Düzenleyici")
            {%>
        <div class="alert alert-danger text-center w-50 mx-auto" role="alert">
            Rolünüz bu sayfa için uygun değildir. Lütfen uygun rol ile giriş yapınız.
        </div>
        <div class="text-center">
            <asp:HyperLink CssClass="btn btn-primary" NavigateUrl="~/AdminPanel/Giris.aspx" Text="Giriş Yap" runat="server" />
        </div>
        <% 
            }
            else
            { %>

        <table class="table">
            <thead>
                <tr>
                    <th scope="col">Yorum ID</th>
                    <th scope="col">Kullanıcı Adı</th>
                    <th scope="col">Yorum Metni</th>
                    <th scope="col">Yorum Tarihi</th>
                    <th scope="col">Video Adı</th>
                    <th scope="col">Durum</th>
                    <th scope="col" class="text-center">İşlemler</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptYorumlar" runat="server" OnItemCommand="rptYorumlar_ItemCommand" OnItemDataBound="rptYorumlar_ItemDataBound">
                    <ItemTemplate>
                        <tr>
                            <th scope="row"><%# Eval("YorumID") %></th>
                            <td><%# Eval("KullaniciTakmaAd") %></td>
                            <td><%# Eval("YorumMetni") %></td>
                            <td><%# Eval("YorumTarihi") %></td>
                            <td><%# Eval("VideoBaslik") %></td>
                            <td>
                                <%# (bool)Eval("YorumDurum") ? "Onaylandı" : "Onaylanmadı" %>
                            </td>
                            <td class="text-center">
                                <a href='<%# "Sil.aspx?id=" + Eval("YorumID") + "&sorgu=DELETE FROM Yorumlar WHERE YorumID = @ID&type=yorum" %>' class="btn btn-danger btn-sm" onclick="return confirm('Bu yorumu silmek istediğinizden emin misiniz?')">Sil</a>
                                <asp:Button ID="btnOnayla" runat="server" CssClass="btn btn-success btn-sm" CommandName="Onayla" CommandArgument='<%# Eval("YorumID") %>' Text="Onayla" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
        <div class="text-center">
            <strong>Toplam Yorum Sayısı: </strong>
            <asp:Label ID="lblToplamYorum" runat="server" Text="0"></asp:Label>
        </div>
        <% } %>
    </form>
</asp:Content>
