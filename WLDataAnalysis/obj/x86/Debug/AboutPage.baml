   M S B A M L   `   `   `  ����  WLDataAnalysis    WLDataAnalysis.AboutPage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      -   5          � 	Resources  5         showStoryBoard �� TargetProperty �� 
TargetName<  6System.Windows.Media.Animation.EasingThicknessKeyFrame p� KeyTime p� Value  HideStoryBoard��%�  &	        &	 �     �� 5	   
   .��5
      r� $ (FrameworkElement.Margin)�6/   $ 
txtLoading��6e   .	�5        $ 0:0:0��6*   $ 0,0,0,0q�6:     5      $ 0:0:1��6*   $ 0,0,0,70q�6:   5      5      �� 5   
   .��5      r� $ (FrameworkElement.Margin)�6/   $ 
txtLoading��6e   .	�5        $ 0:0:0��6*   $ 0,0,0,70q�6:     5      $ 0:0:1��6*   $ 	0,0,0,150q�6:   5      5      5      .��5      � ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ 	#FFB9BBFF��6    � Offset$ 0Q�61   � 5      $ White��6   $ 1Q�6-   � 5      $ White��6   $
 0.72Q�6-   5      .+�5    
   �� $��1��6   $��1��6   $ ��Images/AboutBackground.png��6-   	 J� Opacity$		 0.4Q�6Q   $
��Fill=�6_   � 5!   
   
 � ColumnDefinitions
 5"      �� 5#      $��* �6#   �� 5$      $��5* �6#    � RowDefinitions 5&      �� 5'      $��* �6    �� 5(      $��2* �6    �� 5)      $��2* �6    .+�5,      �� $	��Top=�6   $0��*/WLDataAnalysis;component/Images/cubes.png��6,   � 5-      $��2��6   $��1��6    $��Stretch=�60   $��Stretch=�6L   ���	#00FFFFFF J� OpacityMask 5.      �� 5/      $ 0.5,1&�6*   $ 0.5,0&�6;   .��50      � $ 	#00000000��6'   $ 0.283Q�69   � 51      $ 	#00000000��6'   $ 0.778Q�69   � 52      $ White��6'   $ 0.491Q�65   53      .+�55      �� -   5         ��Name$ 
txtLoading��55       �� TextAlignment$
 Left=�60   $��16(�6E   $��Normal"�6S   ���	#FF052045$��Beta 0.9��6�   $
��Wrap=�6�   $��Bottom=�6�   $��Right=�6�   $	��421��6�    J� Effect 56      3  -System.Windows.Media.Effects.DropShadowEffect  57         
BlurRadius$ 6Q�6+      Color$ 	#FF797979��6:      	Direction$	 201Q�6L      Opacity$
 0.68Q�6\      ShadowDepth$ 3Q�6k   59      5:      �� 5<      $��1��6    �� Orientation$ Vertical=�6)   $��Center=�6@   .+�5=      �� $ Center=�6   $
��Left=�63   $��Water Level Data Analysis��6N   
�������$
��Bold"�6�   $��35(�6�   $��49��6�    5>        5?      $ 	#FF3E3E3E��6+   $	 201Q�6=   $
 0.68Q�6M   $ 3Q�6\   $ 6Q�6l   5A      �� 5B      $��22(�6   $
��Bold"�6*   
�������$(��"Software Version: 1.7.6 Build 2160��6O   $
��Wrap=�6y    J� RenderTransformOrigin$ -0.406,0.368&�6�    5C        5D      $ 6Q�6+   $ 	#FF797979��6:   $	 201Q�6L   $
 0.68Q�6\   $ 3Q�6k   5E   *   5F      �� 5H      $ Vertical=�6   $��1��60   $��1��6>   $��0,25,0,0q�6N   .+�5I      �� $��Copyright © 2014��6   $��16(�64   
�������$
��Bold"�6U   &   System.Windows.Media.TextOptions   TextFormattingMode$ Display=�6g    5J        5K      $ 6Q�6+   $ 	CadetBlue��6:   $	 213Q�6L   $
 0.68Q�6\   $ 5Q�6k      RenderingBias$ Quality=�6{   5L   *   �� 5M      $��16(�6   
�������$��By: Babak Akbarzadeh��6=   $
 Left=�6Y   $
��Bold"�6n   $��0,15,0,0q�6�   $ Display=�6�    5N        5O      $ 6Q�6+   $ 	#FF797979��6:   $	 213Q�6L   $
 0.68Q�6\   $ 3Q�6k   $ Quality=�6{   5P   *   �� 5Q      $��16(�6   
�������$$��Pouya Tarh Pars Consultant Co.��6=   $
 Left=�6c   $
��Bold"�6x   $ Display=�6�    5R        5S      $ 6Q�6+   $ 	#FF797979��6:   $	 213Q�6L   $
 0.68Q�6\   $ 3Q�6k   $ Quality=�6{   5T   *   5U      5V      5W      5X      