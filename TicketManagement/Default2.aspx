<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
        ChatDiv {
            display: inline-block;
            max-width: 200px;
            background-color: white;
            padding: 5px;
            border-radius: 4px;
            position: relative;
            border-width: 1px;
            border-style: solid;
            border-color: grey;
        }

        left {
            float: left;
        }

        ChatDiv.left:after {
            content: "";
            display: inline-block;
            position: absolute;
            left: -8.5px;
            top: 7px;
            height: 0px;
            width: 0px;
            border-top: 8px solid transparent;
            border-bottom: 8px solid transparent;
            border-right: 8px solid white;
        }

        ChatDiv.left:before {
            content: "";
            display: inline-block;
            position: absolute;
            left: -9px;
            top: 7px;
            height: 0px;
            width: 0px;
            border-top: 8px solid transparent;
            border-bottom: 8px solid transparent;
            border-right: 8px solid black;
        }

        ChatDiv.right:after {
            content: "";
            display: inline-block;
            position: absolute;
            right: -8px;
            top: 6px;
            height: 0px;
            width: 0px;
            border-top: 8px solid transparent;
            border-bottom: 8px solid transparent;
            border-left: 8px solid #dbedfe;
        }

        ChatDiv.right:before {
            content: "";
            display: inline-block;
            position: absolute;
            right: -9px;
            top: 6px;
            height: 0px;
            width: 0px;
            border-top: 8px solid transparent;
            border-bottom: 8px solid transparent;
            border-left: 8px solid black;
        }

        right {
            float: right;
            background-color: #dbedfe;
        }

        .clear {
            clear: both;
        }
    </style>

</head>
<body>
   
    <div class="ChatDiv left">left</div>
    <div class="clear"></div>
    <div class="ChatDiv right">right</div>
  
</body>
</html>
