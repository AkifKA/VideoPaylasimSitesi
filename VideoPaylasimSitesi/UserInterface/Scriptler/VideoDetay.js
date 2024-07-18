
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
                showSuccess('Yorum başarıyla eklendi.');
                __doPostBack('UpdatePanel1', '');
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
