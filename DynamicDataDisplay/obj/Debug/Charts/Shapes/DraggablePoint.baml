   M S B A M L   `   `   `  ����Y  UDynamicDataDisplay, Version=0.3.0.0, Culture=neutral, PublicKeyToken=5b7d744a7263923fk:clr-namespace:Microsoft.Research.DynamicDataDisplay.Charts,Microsoft.Research.DynamicDataDisplay.Charts  5      H    BMicrosoft.Research.DynamicDataDisplay.Charts.Shapes.DraggablePoint   5      \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      Gd3:clr-namespace:Microsoft.Research.DynamicDataDisplay.Charts    5        � ToolTip  5      �� *
Position+ �� RelativeSource �� *Self+��5      �� 5       �� 
TargetTypeF   @Microsoft.Research.DynamicDataDisplay.Charts.ViewportUIContainer"  6
    �� 	Resources 5	       	  story �� TargetProperty W� From	 W� To g� Duration g� AutoReverse	 g� RepeatBehavior��%�   &	        �� 5
      .��5      W� $ Opacity�6   $ 1X�5      $	 0.1X�5      $ 0:0:0.4D�6    . $	 Forever��5      5      5      .�5      �� 
 �� Property
 �@J�Opacity �� Value$ 1Q�6   �� 5      
 �@�Cursor$ 	ScrollAll��6   �� 5      
 � ��$ Center=�6;   �� 5      
 � ��$ Center=�69    �� Triggers 5      ]� 5       ]� 
Conditions 5      �� 5       �� Property �@J�IsMouseOver �� Value . 6)   �� 5       �@J�IsMouseCaptured .  6-   /�5       N� EnterActions 5      �� 5        ��Name$ 
storyboard��6   #��[  6*    N� ExitActions 5"      �� 5#       �� BeginStoryboardName$ 
storyboard��6   5%      5'      .��5*      � $��12��6   $��12��6   .+�5+      A� $��3��6   $��3��6   
���   �6!   A� 5,      $��12��6   $��12��6   
������ 
���   �66   5-      5.      