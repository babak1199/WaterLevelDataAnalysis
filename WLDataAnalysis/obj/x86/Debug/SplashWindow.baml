   M S B A M L   `   `   `  ����  WLDataAnalysis!    WLDataAnalysis.SplashWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  splash��5   	   $	��310��6   $	��459��6%    � WindowStyle$
 None=�5   	    � WindowStartupLocation$ CenterScreen=�6    � 
ResizeMode$ NoResize=�6A    � ShowInTaskbar .   � Topmost .  � 	Resources 5         showStoryBoard �� TargetProperty �� 
TargetName<  6System.Windows.Media.Animation.EasingThicknessKeyFrame	 p� KeyTime
 p� Value  HideStoryBoard��%�  &	        &	 �     �� 5   
   .��5      r� $ (FrameworkElement.Margin)�6/   $ 
txtLoading��6e   .	�5	        $	 0:0:0��6*   $
 0,0,0,0q�6:     5
      $	 0:0:1��6*   $
 0,0,0,70q�6:   5      5      �� 5   
   .��5      r� $ (FrameworkElement.Margin)�6/   $ 
txtLoading��6e   .	�5        $	 0:0:0��6*   $
 0,0,0,70q�6:     5      $	 0:0:1��6*   $
 	0,0,0,150q�6:   5      5      5      .��5      � ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ 	#FFB9BBFF��6    � Offset$ 0Q�61   � 5      $ White��6   $ 1Q�6-   � 5      $ White��6   $
 0.72Q�6-   5      .+�5   
   �� $��177,21,10,104q�6   $��Images/timeseries.png��6'   $
��Fill=�6F   �� 5   
   $	��100��6   $	��100��6   $��0,50,359,160q�6)   $0��*/WLDataAnalysis;component/Images/cubes.png��6?   � 5   
   $��Bottom=�6   $��Stretch=�6*   $	��130��6H   ���	#00FFFFFF � ColumnDefinitions 5       �� 5!      $
��224* �6#   �� 5"      $
��235* �6#    J� OpacityMask 5$      �� 5%      $ 0.5,1&�6&   $ 0.5,0&�67   .��5&      � $ 	#00000000��6#   $ 0.283Q�65   � 5'      $ 	#00000000��6#   $ 0.778Q�65   � 5(      $ White��6#   $ 0.491Q�61   5)      .+�5+      �� -   5         ��Name$ 
txtLoading��5+       �� TextAlignment$
 Left=�6,   $��16(�6A   $��Normal"�6O   ���	#FF052045$��Beta 0.9��6|   $
��Wrap=�6�   $��Bottom=�6�   $��Right=�6�   $	��421��6�   $��2��6�   $��0,0,0,-1q�6�    J� Effect 5,      3  -System.Windows.Media.Effects.DropShadowEffect  5-         
BlurRadius$ 6Q�6'      Color$ 	#FF797979��66      	Direction$	 201Q�6H      Opacity$
 0.68Q�6X      ShadowDepth$ 3Q�6g   5/      50      �� 52   
   $ Center=�6   $
��Left=�6+   $��	26,10,0,0q�6F   $��Water Level Data Analysis��6Y   $	��Top=�6z   
�������$
��Bold"�6�   $	��384��6�   $��30(�6�   $��49��6�    53        54      $ 	#FF3E3E3E��6#   $	 201Q�65   $
 0.68Q�6E   $ 3Q�6T   $ 6Q�6d   56      �� 57   
   $��16(�6   $
��Bold"�6"   
�������$��Center=�6G   $��276,59,49,0q�6d   $��version 1.7.6��6y   $ Center=�6�   $
��Wrap=�6�   $	��Top=�6�   $	��134��6�    58        59      $ 6Q�6#   $ 	#FF797979��62   $	 201Q�6D   $
 0.68Q�6T   $ 3Q�6c   5:   "   �� 5;   
   $��16(�6   ���	#FF3A4FCF$
��Left=�69   $��
56,155,0,0q�6T   $$��Pouya Tarh Pars Consultant Co.��6h   $
 Left=�6�   $	��Top=�6�   $	��245��6�   $
��Bold"�6�   &   System.Windows.Media.TextOptions   TextFormattingMode$ Display=�6�    J� RenderTransformOrigin$ 	0.3,0.279&�6   5<        5=      $ 6Q�6#   $ 	#FF797979��62   $	 213Q�6D   $
 0.68Q�6T   $ 3Q�6c      RenderingBias$ Quality=�6s   5>   "   �� 5?   
   $��16(�6   ���	#FF3A4FCF$
��Left=�69   $��
56,134,0,0q�6T   $��By: Babak Akbarzadeh��6h   $
 Left=�6�   $	��Top=�6�   $	��175��6�   $
��Bold"�6�   $ Display=�6�    5@        5A      $ 6Q�6#   $ 	#FF797979��62   $	 213Q�6D   $
 0.68Q�6T   $ 3Q�6c   $ Quality=�6s   5B   "   5C      5D      