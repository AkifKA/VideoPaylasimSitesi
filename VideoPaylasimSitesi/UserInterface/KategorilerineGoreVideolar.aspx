<%@ Page Title="" Language="C#" MasterPageFile="~/UserInterface/User.Master" AutoEventWireup="true" CodeBehind="KategorilerineGoreVideolar.aspx.cs" Inherits="VideoPaylasimSitesi.UserInterface.KategorilerineGoreVideolar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="site_head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="site_content" runat="server">
    <!-- Alt Kategorisine Göre Video Serileri -->
    <asp:Repeater ID="rptBaslik" runat="server">
        <ItemTemplate>
            <h1 class="text-center" style="margin-top:10px;"><%# Eval("AltKatAciklama") %></h1>
        </ItemTemplate>
    </asp:Repeater>
    <div class="row mt-4 justify-content-center ">
        <% if (VideoRows > 0)
            {%>
        <asp:Repeater ID="rptVideo" runat="server">
            <ItemTemplate>
                <div class="col-md-4 mb-4">
                    <div class="card h-100">
                        <img class="card-img-top fixed-size" src='<%# Eval("VideoUrl") %>' alt='<%# Eval("VideoUrl") %>'>
                        <div class="card-body d-flex flex-column justify-content-between flex-grow-1">
                            <h5 class="card-title"><%# Eval("VideoBaslik") %></h5>
                            <p class="card-text"><%# Eval("VideoAciklama") %></p>
                            <a href='VideoDetay.aspx?VideoID=<%# Eval("VideoID") %>' class="btn btn-outline-primary">İzle</a>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <%}
            else
            {%>
        <div class="alert alert-warning w-50 text-center mx-auto" role="alert">
            Bu kategoriye ait video, henüz bulunmamaktadır.
        </div>
        <button class="btn btn-outline-danger" style="width: 100px;" onclick="history.back()">Geri <<<</button>
        <%} %>
    </div>
    <div class="row mx-auto justify-content-center">
        <button class="btn btn-outline-success" style="width: 100px;" onclick="history.back()">Geri <<<</button>
    </div>
</asp:Content>
