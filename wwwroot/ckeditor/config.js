/**
 * @license Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.html or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    config.language = 'vi'; // Đặt ngôn ngữ là tiếng Việt (tùy chọn)
    config.uiColor = '#AADC6E'; // Màu nền của UI (tùy chọn)

    // Cấu hình chế độ Enter thành thẻ <br> thay vì <p>
    config.enterMode = CKEDITOR.ENTER_BR;

    // Cấu hình thanh công cụ
    config.toolbar = 'Full';

    // Cấu hình đường dẫn đúng đến CKFinder
    config.filebrowserBrowseUrl = '/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/ckfinder/ckfinder.html?type=Images';
    config.filebrowserFlashBrowseUrl = '/ckfinder/ckfinder.html?type=Flash';
    config.filebrowserUploadUrl = '/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';
    config.filebrowserFlashUploadUrl = '/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

    // Cấu hình kích thước cửa sổ CKFinder
    config.filebrowserWindowWidth = '1000';
    config.filebrowserWindowHeight = '700';
};
