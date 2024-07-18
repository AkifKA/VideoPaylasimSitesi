<%@ Page Title="" Language="C#" MasterPageFile="~/UserInterface/User.Master" AutoEventWireup="true" CodeBehind="VideoDetay.aspx.cs" Inherits="VideoPaylasimSitesi.UserInterface.VideoDetay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="site_head" runat="server">
    <!-- Benim CSS Dosyam -->
    <link href="Sitiller/VideoDetay.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.2/css/all.min.css" integrity="sha512-SnH5WK+bZxgPHs44uWIX+LLJAJ9/2PkPKZ5QiAj6Ta86w+fsb2TkcmfRyVX3pBnMFcV7oQPJkl9QevSCWr3W6A==" crossorigin="anonymous" referrerpolicy="no-referrer" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="site_content" runat="server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <% if (Request.Cookies["UserInfo"] != null)
            { %>
        <div class="container mt-4">
            <asp:Repeater ID="rptVideo" runat="server">
                <ItemTemplate>
                    <div class="row">
                        <div class="col-12">
                            <div class="video-container">
                                <iframe id="IframeVideo" runat="server" src='<%# Eval("VideoFrameUrl") %>' frameborder="0" allowfullscreen></iframe>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 baslik">
                            <h3><%# Eval("VideoBaslik") %></h3>
                            <div class="like-icon" id="likeIcon_<%# Eval("VideoID") %>" onclick="toggleLike(<%# Eval("VideoID") %>);">
                                <i class='<%# GetLikeIconClass(Convert.ToBoolean(Eval("IsLiked"))) %> fa-thumbs-up'></i>
                                <span id="LikeCount_<%# Eval("VideoID") %>"><%# Eval("LikeCount") %></span>
                            </div>
                        </div>
                    </div>
                    <div class="row mt-1">
                        <div class="col-12">
                            <p><%# Eval("VideoAciklama") %></p>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <div class="row mt-4">
                <div class="col-12">
                    <div class="user-comment d-flex flex-column">
                        <div class="comment-user-info d-flex align-items-center mb-2">
                            <asp:Image ID="imgUserProfile" runat="server" CssClass="user-profile-image mr-2" />
                            <span class="user-name"><%= GetUserFullName() %></span>
                        </div>
                        <asp:TextBox ID="yorumText" runat="server" CssClass="form-control mb-2"></asp:TextBox>
                        <asp:Button ID="btnYorumYap" runat="server" CssClass="btn btn-primary ml-auto mt-2" Text="Yorum Yap" OnClientClick="addComment(); return false;" />
                    </div>
                </div>

                <!-- Diğer Kullanıcıların Yorumları -->
                <div class="other-comments mt-4">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Repeater ID="rptYorumlar" runat="server" OnItemCommand="rptYorumlar_ItemCommand" OnItemDataBound="rptYorumlar_ItemDataBound">
                                <ItemTemplate>
                                    <div class="user-comment">
                                        <div class="comment-user-info">
                                            <asp:Image ID="imgOtherUserProfile" runat="server" CssClass="user-profile-image" ImageUrl='<%# Eval("KullaniciResUrl") %>' />
                                            <span class="user-name"><%# Eval("KullaniciAdi") + " " + Eval("KullaniciSoyadi") %></span>
                                            <span class="comment-date font-italic ml-3"><%# Convert.ToDateTime(Eval("YorumTarihi")).ToString("dd MMM yyyy HH:mm") %></span>
                                        </div>
                                        <div class="comment-content">
                                            <asp:TextBox ID="lblCommentText" runat="server" Text='<%# Eval("YorumMetni") %>' CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                            <asp:LinkButton ID="btnYorumDuzenle" runat="server" CommandName="Duzenle" CommandArgument='<%# Eval("YorumID") %>' CssClass="btn btn-warning btn-sm" Visible="false">Düzenle</asp:LinkButton>
                                            <asp:LinkButton ID="btnYorumSil" runat="server" CommandName="Sil" CommandArgument='<%# Eval("YorumID") %>' CssClass="btn btn-danger btn-sm" Visible="false">Sil</asp:LinkButton>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnModalKaydet" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnYorumYap" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div id="error-message" class="alert alert-danger" style="display: none;"></div>
        <div id="success-message" class="alert alert-success" style="display: none;"></div>

        <div class="row m-4 justify-content-center">
            <button class="btn btn-outline-success" style="width: 100px;" onclick="history.back()">Geri <<<</button>
        </div>

        <% }
        else
        { %>
        <div class="text-center video-detay">
            <h3>Video Detay Sayfası</h3>
            <p>Video detay sayfası sadece giriş yapmış kullanıcılar için görüntülenebilir.</p>
            <a href="Giris.aspx" class="btn btn-outline-danger">Giriş Yap</a>
        </div>
        <% } %>

        <!-- Düzenleme Modali -->
        <div class="modal fade" id="editModal" tabindex="-1" role="dialog" aria-labelledby="editModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="editModalLabel">Yorumu Düzenle</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:TextBox ID="txtModalEditComment" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:HiddenField ID="hfEditingYorumID" runat="server" />
                        <asp:Button ID="btnModalKaydet" runat="server" CssClass="btn btn-primary mt-2" OnClientClick="saveComment(); return false;" Text="Kaydet" />
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <!-- JQUERY Fonksiyonları -->

    <script>
        function setCommentText(commentText) {
            document.getElementById('<%= txtModalEditComment.ClientID %>').value = commentText;
        }

        function openEditModal() {
            $('#editModal').modal('show');
        }

        function closeEditModal() {
            $('#editModal').modal('hide');
        }

        function editComment(yorumID, yorumMetni) {
            $('#<%= hfEditingYorumID.ClientID %>').val(yorumID);
            $('#<%= txtModalEditComment.ClientID %>').val(yorumMetni);
            openEditModal();
        }

        $(document).ready(function () {
            var profileImageUrl = '<%= GetUserProfileImageUrl() %>';
            $('#<%= imgUserProfile.ClientID %>').attr('src', profileImageUrl);

            // Handle the modal close button
            $('#editModal .close').on('click', function () {
                closeEditModal();
            });
        });

        function toggleLike(videoID) {
            $.ajax({
                type: "POST",
                url: "VideoDetay.aspx/ToggleLike",
                data: JSON.stringify({ videoID: videoID }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.error) {
                        console.error("Hata: ", response.error);
                        alert("Bir hata oluştu: " + response.error);
                        return;
                    }

                    var data = JSON.parse(response.d);
                    var likeCount = data.likeCount;
                    var isLiked = data.isLiked;

                    $('#LikeCount_' + videoID).text(likeCount);
                    var likeIcon = $('#likeIcon_' + videoID + ' i');
                    if (isLiked) {
                        likeIcon.removeClass('fa-regular').addClass('fa-solid');
                    } else {
                        likeIcon.removeClass('fa-solid').addClass('fa-regular');
                    }
                },
                error: function (xhr, status, error) {
                    console.error("Hata: ", error);
                    alert("Bir hata oluştu: " + error);
                }
            });
        }

        function addComment() {
            var videoID = '<%= Request.QueryString["VideoID"] %>';
            var yorumMetni = $('#<%= yorumText.ClientID %>').val();
            $.ajax({
                type: "POST",
                url: "VideoDetay.aspx/AddComment",
                data: JSON.stringify({ videoID: videoID, yorumMetni: yorumMetni }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.error) {
                        showError(response.error);
                    } else {
                        $('#<%= yorumText.ClientID %>').val('');
                        alert('Yorumunuz yönetici onayından sonra yayınlanacaktır.');
                        __doPostBack('UpdatePanel1', '');

                        // Kullanıcı resmini güncelle
                        var profileImageUrl = '<%= GetUserProfileImageUrl() %>';
                        $('#<%= imgUserProfile.ClientID %>').attr('src', profileImageUrl);
                    }
                },
                error: function (xhr, status, error) {
                    showError("Bir hata oluştu: " + error);
                }
            });
        }

        function saveComment() {
            var yorumID = $('#<%= hfEditingYorumID.ClientID %>').val();
            var yorumMetni = $('#<%= txtModalEditComment.ClientID %>').val();

            if (!yorumID) {
                showError("Yorum ID boş.");
                return;
            }

            $.ajax({
                type: "POST",
                url: "VideoDetay.aspx/UpdateComment",
                data: JSON.stringify({ yorumID: parseInt(yorumID, 10), yorumMetni: yorumMetni }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.error) {
                        console.error("Sunucu hatası:", response.error);
                        showError("Bir hata oluştu: " + response.error);
                    } else {
                        showSuccess('Yorum başarıyla güncellendi.');
                        closeEditModal();
                        __doPostBack('UpdatePanel1', '');
                    }
                },
                error: function (xhr, status, error) {
                    console.error("AJAX hatası:", error);
                    console.error("Response Text:", xhr.responseText);
                    showError("Bir hata oluştu: " + error);
                }
            });
        }

        function showError(message) {
            $('#error-message').text(message).show();
            setTimeout(function () {
                $('#error-message').hide();
            }, 5000);
        }

        function showSuccess(message) {
            $('#success-message').text(message).show();
            setTimeout(function () {
                $('#success-message').hide();
            }, 5000);
        }
    </script>
</asp:Content>
