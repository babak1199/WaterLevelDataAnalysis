   M S B A M L   `   `   `  ����M  IWpfPageTransitions, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS<clr-namespace:WpfPageTransitions;assembly=WpfPageTransitionsWpfPageTransitions  5       WLDataAnalysis   WLDataAnalysis.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      XpageTransitions<clr-namespace:WpfPageTransitions;assembly=WpfPageTransitions     5      -   5          � Title$  Water Level Data Analysis��5      $	��600��6'   $	��800��64    � Icon$ TimeSeries2.ico��6W    � 	Resources 5         ExpanderToggleButton �� 
TargetType �� CornerRadius  NormalBorderBrush  
GlyphBrush �� Triggers P� Property P� Value �� 
TargetName  	DarkBrush	 �� Value
 �� Property  PressedBrush  DisabledBackgroundBrush  DisabledBorderBrush  DisabledForegroundBrush �� 
TargetType � RowDefinitions �Name  
LightBrush � ColumnDefinitions � OverridesDefaultStyle 	 NormalBrush d� 	IsChecked �� Path �� Mode �� RelativeSource 
 WindowBackgroundBrush  SolidBorderBrush �� ElementName  ButtonFocusVisual ��Name  DefaultedBorderBrush  PressedBorderBrush �� EndPoint �� 
StartPoint � Color � Offset'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState   
Storyboard g� Duration �� 
TargetName �� TargetProperty	  W� To	! �� To	" ,� To# J� Opacity$ ��Name% ��Name& �� RadiusX' �� RadiusY( J� IsHitTestVisible��%�  &	        7  7  7  7  7  7  7  7  '9� f    7  7  7   7 	 7 
 7  7  7  7  '�� �    7  7 	 7  7  7  7  7  7  7  7  7  'd� o    �� 5   
   " d�67   .R�5	   
   �� $��Border��5
      $ 2,0,0,0��5      
������ 8��  $��0,0,1,0q�5      ._�5      F� $��Arrow��5      8�� $��Center=�5      $��Center=�5      "����        � 	=  	= � z     	5      5       5   
   P� 5       �@J�IsMouseOver . . �5      �� $ Border��6   8	  
 � ��5      5      P� 5       � �� . . �5      �� $ Border��6   8	  
 � ��5      5      P� 5       �@d�	IsChecked$
 trueY�6+   . �5       �� $ Arrow��6   
 � ��"	 ��     	= � 	=     � z  	= 	5!      5"      P� 5#       � {� .  . �5$      �� $ Border��6   8	  
 � ��5%      �� 5&      $ Border��6   8	  
 � ��5'      �� 5(      8	  
 � ��5)      �� 5*      $ Arrow��6   8	  
 � ��5+      5,      5.      �� 50   
   " 9�6   .�51   
   �� 
 � ��	 52      �� 53      " 9�6"   .R�54      �  55      �� 56      $
��Auto �6,   �� 57      $ 
ContentRow��6,   $��0 �6>   .+�59      �� $��Border��5:      $��0��5;      8��  8�� $��1q�5>      $ 2,2,0,0��5?      ._�5@      �  5A   "   �� 5B   &   $��20 �67   �� 5C   &   $��* �67   .+�5E   "   d�  . 8�� 8��  5F      �� $ 
IsExpanded�$ TwoWay=� �� *TemplatedParent+5J      �� 5K   "   $��1��5L      $��4q�5M      $��Header��5N      ��. 5O      5P      5Q      �� 5R      $��Content��5S      $��1��5T      8�� 8�� $��1,0,1,1q�5W      $ 0,0,2,2��5X      ._�5Y      �� $��4q�6/   5Z      5[       5\      P� 5]       �@9�
IsExpanded . . �5^      �� $ 
ContentRow��6%   	 5_      �� $ Content��$ DesiredHeight�
 � ��5`      P� 5a       � {� .  . �5b      �� $ Border��6%   8	  
 � ��5c      �� 5d      $ Border��6%   8	  
 � ��5e      �� 5f      8	  
 � ��5g      5h      5k      5m      5n      �� 5p   
   " ��6   .�5q      �� 
 �@J�SnapsToDevicePixels	 . 64   �� 5r      
 �@�OverridesDefaultStyle	 . 66   �� 5s      
 �@�FocusVisualStyle8	   61   �� 5t      
 � ��$	 23��6*   �� 5u      
 � ��$	 75��6)   �� 5v      
 � ��	 5w      �� 5x      " ��6&   .R�5y      �� $ Border��5z      $ 2��5{      $��1q�5|      8�� 8�� ._�5      �� $��2q�5�      $��Center=�5�      $��Center=�5�      ��. 5�      5�       5�      P� 5�       �@J�IsKeyboardFocused . . �5�   "   �� $ Border��6)   8	  
 � ��6T   5�      P� 5�       �@��IsDefaulted . . �5�   "   �� $ Border��6)   8	  
 � ��6T   5�      P� 5�       �@J�IsMouseOver . . �5�   "   �� $ Border��6)   8	  
 � ��6S   5�      P� 5�       � �� . . �5�   "   �� $ Border��6)   8	  
 � ��6S   �� 5�   "   $ Border��6)   8	  
 � ��6T   5�      P� 5�       � {� .  . �5�   "   �� $ Border��6)   8	  
 � ��6S   �� 5�   "   $ Border��6)   8	 	 
 � ��6T   �� 5�   "   8	 
 
 � ��6?   5�      5�      5�      5�      �� 5�   
   " d�6   .�5�      �� 
 � ��	 �	#FF1F3B536+   �� 5�      
 � ��	 �	#FF0000006+   �� 5�      
 � ��$	 3q�6(   �� 5�      
 � ��$	 1q�60   �� 5�      
 � ��	 5�      �� 5�      $ 0.5,1&�6*   $ 0.5,0&�6;   .��5�      � $ 	#FFA3AEB9��6'   $ 0Q�69   � 5�      $ 	#FF8399A9��6'   $ 0.375Q�69   � 5�      $ 	#FF718597��6'   $ 0.375Q�69   � 5�      $ 	#FF617584��6'   $ 1Q�69   5�      5�      �� 5�      
 � ��	 5�      �� 5�      " d�6&   .R�5�      �  5�        5�   "   CommonStates��. 5�   &     
Normal��62     5�   &   	MouseOver��. 5�   *   �� .��5�   .   W� $ 0D�6>   $ BackgroundAnimation��6K   $ Opacity�6w   $  1X�6�   �� 5�   .   $ 0D�6=   $ BackgroundGradient��6J   $L F(Rectangle.Fill).(GradientBrush.GradientStops)[1].(GradientStop.Color)�6u   $! 	#F2FFFFFFX�6�   �� 5�   .   $ 0D�6=   $ BackgroundGradient��6J   $L F(Rectangle.Fill).(GradientBrush.GradientStops)[2].(GradientStop.Color)�6u   $! 	#CCFFFFFFX�6�   �� 5�   .   $ 0D�6=   $ BackgroundGradient��6J   $L F(Rectangle.Fill).(GradientBrush.GradientStops)[3].(GradientStop.Color)�6u   $! 	#7FFFFFFFX�6�   5�   +   5�   '     5�   &   Pressed��. 5�   *   �� .��5�   .   �� $ 0D�6=   $ 
Background��6J   $1 +(Border.Background).(SolidColorBrush.Color)�6m   $! 	#FF6DBDD1X�6�   W� 5�   .   $ 0D�6>   $ BackgroundAnimation��6K   $ Opacity�6w   $  1X�6�   �� 5�   .   $ 0D�6=   $ BackgroundGradient��6J   $L F(Rectangle.Fill).(GradientBrush.GradientStops)[0].(GradientStop.Color)�6u   $! 	#D8FFFFFFX�6�   �� 5�   .   $ 0D�6=   $ BackgroundGradient��6J   $L F(Rectangle.Fill).(GradientBrush.GradientStops)[1].(GradientStop.Color)�6u   $! 	#C6FFFFFFX�6�   �� 5�   .   $ 0D�6=   $ BackgroundGradient��6J   $L F(Rectangle.Fill).(GradientBrush.GradientStops)[2].(GradientStop.Color)�6u   $! 	#8CFFFFFFX�6�   �� 5�   .   $ 0D�6=   $ BackgroundGradient��6J   $L F(Rectangle.Fill).(GradientBrush.GradientStops)[3].(GradientStop.Color)�6u   $! 	#3FFFFFFFX�6�   5�   +   5�   '     5�   &   Disabled��. 5�   *   �� .��5�   .   W� $ 0D�6>   $ DisabledVisualElement��6K   $ Opacity�6y   $	  .55X�6�   5�   +   5�   '   5�   #     5�   "   CheckStates��. 5�   &     Checked��. 5�   *   �� .��5�   .   ,� $ 0D�6=   $" 0.7,1X�6J   $ BackgroundGradient��6U   $7 1(Rectangle.Fill).(LinearGradientBrush.StartPoint)�6�   ,� 5�   .   $ 0D�6=   $" 0.7,0X�6J   $ BackgroundGradient��6U   $5 /(Rectangle.Fill).(LinearGradientBrush.EndPoint)�6�   5�   +   5�   '     5�   &   	Unchecked��62   5�   #     5�   "   FocusStates��. 5�   &     Focused��. 5�   *   �� .��5�   .   W� $ 0D�6>   $ FocusVisualElement��6K   $ Opacity�6v   $  1X�6�   5�   +   5�   '     5�   &   	Unfocused��62   5�   #   .+�5�      �� $ 
Background��6%   $ 3��69   
�������#��z��6]   #��z��6�   ._�5�   "   � #��z��6'   $��1q�6R   .+�5�   &   �� $ BackgroundAnimation��6:   $# 0Q�6-   ���	#FF448DCA6W   �� 5�   &   $$ BackgroundGradient��60   ��5�   *   �� 5�   .   $
 .7,0&�6B   $
 .7,1&�6T   .��5�   2   � $ 	#FFFFFFFF��6?   $ 0Q�6Q   � 5�   2   $ 	#F9FFFFFF��6?   $ 0.375Q�6Q   � 5�   2   $ 	#E5FFFFFF��6?   $ 0.625Q�6Q   � 5�   2   $ 	#C6FFFFFF��6?   $ 1Q�6Q   5�   /   5�   '   5�   #   5�      �� 5�      $% contentPresenter��5�      #��z��5�      #��z��5�      #��z��5�      #��z��5�      #��z��5�      �� 5�      $$ DisabledVisualElement��6(   $& 3��6G   $' 3��6S   ���	#FFFFFFFF$# 0Q�6p   ( .  6|   �� 5�      $$ FocusVisualElement��6(   $& 2��6D   $' 2��6P   $��1q�6\   ���	#FF6DBDD1$��1��6z   $# 0Q�6�   ( .  6�   5�      5�      5�      5�      5�      .��5�      �  5�   
   �� 5�      $
��Auto �6   �� 5      $��* �6    5  
   �� 5     $
��Auto �6   �� 5     $��* �6   .+�5  
   '   !WpfPageTransitions.PageTransition  -   5        $��pageTransitionControl��5  )   $��1��6F   $��1��6S   )   TransitionType$) SlideAndFade=�6c   �� 5	  
   $��1��6   $��0��6$   .��5
     �� .+�5     �� * �� TextAlignment$* Center=�6   $��Center=�63   $��Center=�6P   $��Water Level Data Analysis��6k   
���<��$
��Bold"�6�   $��20(�6�   + J� Effect+ 5     3  -System.Windows.Media.Effects.DropShadowEffect  5     ,   Color$, 	#FF3E3E3E��6+   -   	Direction$	- 201Q�6=   .   Opacity$
. 0.68Q�6M   /   ShadowDepth$/ 3Q�6\   0   
BlurRadius$0 6Q�6l   5     9� 5     $��Import Data��6   1 9� 
IsExpanded1 . 
���   �$��2q�6V   $��	0,10,0,10q�5  #   .��5     �� $��5,5,5,5q�6!   .+�5     �� -   5        $
��Open��5  !   2 � ToolTip$$2 Select an input file to import��6J   5     5     9� 5     $��Validate Data��6   1 .  
���   �$��2q�6Y   $��	0,10,0,10q�5     .��5     �� $��5,5,5,5q�6!   .+�5     �� $��Method��6#   .��5     �� .+�5  "   	� -   5        $��chkFromSensorElevation��5  .   $��From Sensor Elevation��6L   $��0,5,0,5q�6l   6}   	� 5  "   -   5        $��chkFromCurveAverage��5  .   $��From Curve Average��6I   $
 TrueY�6f   $��0,0,0,5q�6w   6�   5     5     �� 5      $��
Parameters��6#   .��5!     �� .+�5"  "   ]� -   5        $��pSensorElevation��5"  ,   {�.  $��0,5,0,5q�6V   .+�5#  &   �� -   5        $��tSensorElevation��5#  0   $��Sensor Elevation:��6H   �� 5$  &   3 �� Orientation$3 Vertical=�61   .+�5%  *   ]� .+�5&  .   	� -   5        $��rbSensorElevationAuto��5&  :   $
��Auto��6W   $
 TrueY�6f   4 	� 	GroupName$4 SensorElevation��6w   6�   �� 5'  .   -	   5        $��tbSensorElevationAuto��5'  6   $�� ��6S   $��50��6[   5 �� TextAlignment$5 Right=�6f   $��Right=�6|   5(  +   ]� 5)  *   .+�5*  .   	� -
   5        $��rbSensorElevationManual��5*  :   $��Manual��6Y   $4 SensorElevation��6j   6�   �� 5+  .   -   5        $��tbSensorElevationManual��5+  6   $��-3.51��6U   $��50��6b   $5 Right=�6m   $��Right=�6�   5,  +   5-  '   5.  #   ]� 5/  "   -   5        $��
pShapeFunc��5/  ,   {�.  $��0,0,0,5q�6P   .+�50  &   �� $��Average Point No:��60   �� 51  &   -   5        $��tbShapeFuncPointNo��51  .   $��3��6H   $��50��6Q   $5 Right=�6\   $��Right=�6r   52  #   ]� 53  "   $��0,0,0,5q�6,   .+�54  &   �� $��Max Level Variation:��60   �� 55  &   -   5        $��tbMaximumLevelVariation��55  .   $
��0.30��6M   $��50��6Y   $5 Right=�6d   $��Right=�6z   56  #   57     58     �� 59     -   5        6 ��Name$6 btnCheckInvalid��59  !   $��Check Invalid Data��6:   $��0,5,0,0q�6W   {�.  6�   �� 5:     -   5        $6 btnRefineData��5:  !   $��Remove Invalid Data��68   $��0,5,0,0q�6V   {�.  6�   5;     5<     9� 5=     $��Remove Spikes��6   1 .  
���   �$��2q�6Y   $��	0,10,0,10q�5>  #   .��5?     �� $��5,5,5,5q�6!   .+�5@     �� $��
Parameters��6#   .��5A     �� .+�5B  "   ]� -   5        $��
pThreshold��5B  =   $��0,5,0,5q�6,   {�.  .+�5C  &   �� $��
Threshold:��60   �� 5D  &   -   5        $��tbMaxContinuityThreshold��5D  .   $
��0.06��6N   $��50��6Z   $5 Right=�6e   $��Right=�6{   5E  #   ]� 5F  "   -   5        $��pPercentile��5F  =   $��0,0,0,5q�6,   .+�5G  &   �� $��Percentile:��60   �� 5H  &   $3 
Horizontal=�61   $��Right=�6J   .+�5I  *   �� -   5        $��tbPercentile��5I  2   $��80��6F   $��40��6P   $5 Right=�6[   �� 5J  *   $��%��64   5K  '   5L  #   5M     5N     �� 5O     $��Method��6#   $��0,5,0,0q�63   .��5P     �� .+�5Q  "   	� -   5        $��rbDespikeSpike��5Q  .   $��By Spikes identification��6D   $��0,5,0,5q�6g   $
 TrueY�6x   6�   	� 5R  "   -   5        $��rbDespikeDifference��5R  .   $��By difference calculation��6I   $��0,0,0,5q�6m   6~   	� 5S  "   -   5        $��rbDespikeMovAverage��5S  .   $(��"By Trend Line Spike Identification��6I   $
 TrueY�6v   $��0,0,0,5q�6�   6�   5T     5U     �� 5V     -   5        $6 btnFindSpikes��5V  !   $��Find Spikes��68   $��0,5,0,0q�6N   {�.  6{   �� 5W     -   5        $6 
btnDespike��5W  !   $��Despike��65   $��0,5,0,0q�6G   {�.  6q   5X     5Y     9� 5[     $��Export Results��6   1 .  
���   �$��2q�6Z   $��	0,10,0,10q�5\  #   .��5]     �� $��5,5,5,5q�6!   .+�5^     �� $��Reduce��6#   $��0,5,0,0q�63   .��5_     �� .+�5`  "   	� -   5        $��rbAllResult��5`  .   $��All results��6A   $��0,5,0,5q�6W   $
 TrueY�6h   $4 result��6�   	� 5a  "   -   5        $��rbHourlyResult��5a  .   $��Hourly��6D   $��0,0,0,5q�6U   $4 result��6�   	� 5b  "   -   5        $��rbHourlyLowPassResult��5b  .   $��Hourly (Low-pass)��6K   $��0,0,0,5q�6g   $4 result��6�   	� 5c  "   -   5        $��rbDailyResult��5c  .   $��Daily��6C   $��0,0,0,5q�6S   $4 result��6�   	� 5d  "   -   5        $��rbMonthlyResult��5d  .   $��Monthly��6E   $��0,0,0,5q�6W   $4 result��6�   ]� 5e  "   .+�5f  &   	� -   5        $��rbCustomResult��5f  2   $��Custom��6H   $��Center=�6Y   $4 result��6�   �� 5g  &   -    5        $��pCustomResult��5g  1   $3 
Horizontal=�6F   $��Right=�6_   $��Center=�6{   $y�Hidden=�6�   .+�5h  *   �� -!   5        $��tbCustomResult��5h  2   $��5��6H   $��50��6Q   $5 Right=�6\   �� 5i  *   -"   5        $��cbCustomresult��5i  3   5j  '   5k  #   5l     5m     �� 5o     -#   5        $6 btnWriteOutput��5o  !   $��Export Results ...��69   $��0,5,0,0q�6V   {�.  6�   5p     5q     9� 5r     $��Options��6   1 . 
���   �$��2q�6R   $��	0,10,0,10q�5s  #   .��5t     �� $��5,5,5,5q�6!   .+�5u     �� $��Charts��6#   $��0,5,0,0q�63   .��5v     �� .+�5w  "   �� -$   5        $��chkKeepOldCharts��5w  +   $&�� Keep old chart while drawing new��6C   $��0,5,0,5q�6n   $
 TrueY�6   5x     5y     �� 5z     $��Environment��6#   $��0,5,0,0q�68   .��5{     �� .+�5|  "   �� -%   5        $��chkIsExpanded��5|  +   $#��All expanders open on startup��6@   $��0,5,0,5q�6h   5}     5~     5     5�     �� 5�     -&   5        $6 btnShowHideMarkers��5�     $��Show/Hide Markers��65   $��0,5,0,5q�6Q   {�.  6�   �� 5�     -'   5        $��	Reset All��5�     $��0,0,0,5q�6-   6>   d� 5�     -(   5        $��btnAbout��5�     $��About��6/   $��60��6?   $��0,5,0,5q�6K   6\   5�     5�     5�     5�     