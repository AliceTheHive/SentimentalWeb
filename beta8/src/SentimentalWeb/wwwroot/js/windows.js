function SendToast(toastText) {
    var notifications = Windows.UI.Notifications;

    // Get the toast notification manager for the current app.
    var notificationManager = notifications.ToastNotificationManager;

    // The getTemplateContent method returns a Windows.Data.Xml.Dom.XmlDocument object that contains the toast notification XML content.
    var template = notifications.ToastTemplateType.toastText01;
    var toastXml = notificationManager.getTemplateContent(notifications.ToastTemplateType[template]);

    // Set text
    var toastTextElements = toastXml.getElementsByTagName("text");
    toastTextElements[0].appendChild(toastXml.createTextNode(toastText));

    // Set image
    var toastImageElements = toastXml.getElementsByTagName("image");
    toastImageElements[0].setAttribute("src", "ms-appx:///images/smalllogo.png");
    toastImageElements[0].setAttribute("alt", "App logo");

    // Create a toast notification from the XML, then create a ToastNotifier object to send the toast.
    var toast = new notifications.ToastNotification(toastXml);

    notificationManager.createToastNotifier().show(toast);
};

function SendTile(tileText) {
    var notifications = Windows.UI.Notifications;

    //TO DO - enable queue

    // Get the tile XML for a wide tile
    var template = notifications.TileTemplateType.tileWide310x150ImageAndText01;
    var tileXml = notifications.TileUpdateManager.getTemplateContent(template);

    // Set the text
    var tileTextAttributes = tileXml.getElementsByTagName("text");
    tileTextAttributes[0].appendChild(tileXml.createTextNode(tileText));

    // Set the image
    var tileImageAttributes = tileXml.getElementsByTagName("image");
    tileImageAttributes[0].setAttribute("src", "ms-appx:///images/logo.png");
    tileImageAttributes[0].setAttribute("alt", "App logo");

    // Include bindings for multiple sizes, this adds a medium tile
    var squareTemplate = notifications.TileTemplateType.tileSquare150x150Text04;
    var squareTileXml = notifications.TileUpdateManager.getTemplateContent(squareTemplate);

    var squareTileTextAttributes = squareTileXml.getElementsByTagName("text");
    squareTileTextAttributes[0].appendChild(squareTileXml.createTextNode(tileText));

    // Add the medium tiles payload as a sibling to the wide tile
    var node = tileXml.importNode(squareTileXml.getElementsByTagName("binding").item(0), true);
    tileXml.getElementsByTagName("visual").item(0).appendChild(node);

    // Create the notification
    var tileNotification = new notifications.TileNotification(tileXml);

    // Set an expiration
    var currentTime = new Date();
    tileNotification.expirationTime = new Date(currentTime.getTime() + 600 * 1000);

    // Send the tile notification
    var tileUpdater = notifications.TileUpdateManager.createTileUpdaterForApplication();
    tileUpdater.enableNotificationQueue(true);
    tileUpdater.update(tileNotification);
}

function SendPopup(toastText)
{
    // Create the message dialog and set its content
    var msg = new Windows.UI.Popups.MessageDialog(toastText);

    // Add commands
    msg.commands.append(new Windows.UI.Popups.UICommand("OK"));

    // Set default command
    msg.defaultCommandIndex = 0;

    // Show the message dialog
    msg.showAsync();
}