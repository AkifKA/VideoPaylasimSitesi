<%@ Page Title="" Language="C#" MasterPageFile="~/UserInterface/User.Master" AutoEventWireup="true" CodeBehind="KategorilerineGoreAltKategoriler.aspx.cs" Inherits="VideoPaylasimSitesi.UserInterface.KategorilerineGoreAltKategoriler" %>

<asp:Content ID="Content1" ContentPlaceHolderID="site_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="site_content" runat="server">
    <!-- Kategorisine Göre Alt Kategoriler -->
    <asp:Repeater ID="rptAltKatBaslik" runat="server">
        <ItemTemplate>
            <h1 class="text-center" style="margin-top:10px;"><%# Eval("KatAciklama") %></h1>
        </ItemTemplate>
    </asp:Repeater>
    <div class="row mt-4 justify-content-center">
        <% if (AltKategoriRows > 0)
            {%>
        <asp:Repeater ID="rptAltKat" runat="server">
            <ItemTemplate>
                <div class="col-md-4 mb-4">
                    <div class="card h-100">
                        <img class="card-img-top fixed-size" src='<%# Eval("AltKatResUrl") %>' alt='<%# Eval("AltKatResUrl") %>'>
                        <div class="card-body d-flex flex-column justify-content-between flex-grow-1">
                            <h5 class="card-title"><%# Eval("AltKatAd") %></h5>
                            <a href='KategorilerineGoreVideolar.aspx?AltKatID=<%# Eval("AltKatID") %>' class="btn btn-outline-danger">İncele</a>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <%}
            else
            {%>
        <div class="alert alert-warning w-50 text-center mx-auto" role="alert">
            Bu kategoriye ait alt kategori henüz bulunmamaktadır.
        </div>
        <div class="row mx-auto justify-content-center">
            <button class="btn btn-outline-success" style="width: 100px;" onclick="history.back()">Geri <<<</button>
        </div>

        <%} %>
    </div>
    <div class="row mx-auto justify-content-center">
        <button class="btn btn-outline-success" style="width: 100px;" onclick="history.back()">Geri <<<</button>
    </div>
</asp:Content>
