   M S B A M L   `   `   `  ����S  OAudioPlayerMVVMandNAudio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:AudioPlayerMVVMandNAudioAudioPlayerMVVMandNAudio  5      W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� 
TargetType   MuteAudioCheckbox �� Property �� Value �� 
TargetType ��Name ��Name J� RenderTransformOrigin �� BitmapScalingMode �� EdgeMode	 �� ScaleX
 �� ScaleY �� Angle �� Triggers P� Property P� Value �� 
TargetName :� RoutedEvent �� 
TargetName �� TargetProperty	 W� To g� Duration  LoopCheckbox  ShuffleCheckbox�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml     5      0local&clr-namespace:AudioPlayerMVVMandNAudio  5      %Q  &	        &	 �    &	 b    �� 5      "  ��6   .�5   
   ��  � ��$ Center=�60   �� 5   
    � ��$ Center=�6.   �� 5
   
    � �� 5      �� 5      " ��6"   .R�5      �� $ checkboxBorder��6   
������ ._�5      �� $ checkboxImage��6    $;��5pack://application:,,,/Images/Volume/audio unmute.png��5       $��20��5       $��20��5       $ .5,.5&�5       $��Center=�5       $��Center=�5       $ HighQuality=�5       $ Aliased=�5       z�5      V� 5   "   .~�5   &   �� 
ImageScale��$	 1Q�6I   $
 1Q�6T   �� 5   &   ImageRotate��$ 0Q�6K   5   #   5!      5"       5%      P� 5(       �@d�	IsChecked$
 TrueY�67   . �5)      �� $ checkboxImage��6%    � ��$9 3pack://application:,,,/Images/Volume/audio mute.png��6R   5*      :� 5-      $ 
MouseEnter��6'   .[�5.      �� .��5/   "   �� .��50   &   W� $ 
ImageScale��66   $ (ScaleTransform.ScaleX)�6Y    1.16�   $ 
0:0:0:0.05D�6�   W� 51   &   $ 
ImageScale��66   $ (ScaleTransform.ScaleY)�6Y    1.16�   $ 
0:0:0:0.05D�6�   52   #   53      54      :� 57      $ 
MouseLeave��6'   .[�58      �� .��59   "   �� .��5:   &   W� $ 
ImageScale��66   $ (ScaleTransform.ScaleX)�6Y    16�   $ 	0:0:0:0.1D�6�   W� 5;   &   $ 
ImageScale��66   $ (ScaleTransform.ScaleY)�6Y    16�   $ 	0:0:0:0.1D�6�   W� 5<   &   $ ImageRotate��66   $ (RotateTransform.Angle)�6Z    06�   $ 	0:0:0:0.1D�6�   5=   #   5>      5?      :� 5B      $ PreviewMouseDown��6'   .[�5C      �� .��5D   "   �� .��5E   &   W� $ 
ImageScale��66   $ (ScaleTransform.ScaleX)�6Y    1.56�   $ 	0:0:0:0.1D�6�   W� 5F   &   $ 
ImageScale��66   $ (ScaleTransform.ScaleY)�6Y    1.56�   $ 	0:0:0:0.1D�6�   5G   #   5H      5I      :� 5L      $ PreviewMouseUp��6'   .[�5M      �� .��5N   "   �� .��5O   &   W� $ 
ImageScale��66   $ (ScaleTransform.ScaleX)�6Y    1.16�   $ 	0:0:0:0.1D�6�   W� 5P   &   $ 
ImageScale��66   $ (ScaleTransform.ScaleY)�6Y    1.16�   $ 	0:0:0:0.1D�6�   5Q   #   5R      5S      5V      5Y      5Z      �� 5]      "  ��6   .�5^   
   ��  � ��$ Center=�60   �� 5_   
    � ��$ Center=�6.   �� 5a   
    � �� 5b      �� 5c      " ��6"   .R�5d      �� $ checkboxBorder��6   
������ ._�5g      �� $ checkboxImage��6    $4��.pack://application:,,,/Images/Options/loop.png��5h       $��15��5i       $��15��5j       $ .5,.5&�5k       $��Center=�5l       $��Center=�5m       $ HighQuality=�5n       $ Aliased=�5o       z�5q      V� 5r   "   .~�5s   &   �� 
ImageScale��$	 1Q�6I   $
 1Q�6T   �� 5t   &   ImageRotate��$ 0Q�6K   5u   #   5x      5y       5|      P� 5       �@d�	IsChecked$
 TrueY�67   . �5�      �� $ checkboxBorder��6%    � ��
 �  ��6W   �� 5�      $ checkboxImage��6%    � ��$4 .pack://application:,,,/Images/Options/loop.png��6R   5�      :� 5�      $ 
MouseEnter��6'   .[�5�      �� .��5�   "   �� .��5�   &   W� $ 
ImageScale��66   $ (ScaleTransform.ScaleX)�6Y    1.16�   $ 
0:0:0:0.05D�6�   W� 5�   &   $ 
ImageScale��66   $ (ScaleTransform.ScaleY)�6Y    1.16�   $ 
0:0:0:0.05D�6�   5�   #   5�      5�      :� 5�      $ 
MouseLeave��6'   .[�5�      �� .��5�   "   �� .��5�   &   W� $ 
ImageScale��66   $ (ScaleTransform.ScaleX)�6Y    16�   $ 	0:0:0:0.1D�6�   W� 5�   &   $ 
ImageScale��66   $ (ScaleTransform.ScaleY)�6Y    16�   $ 	0:0:0:0.1D�6�   W� 5�   &   $ ImageRotate��66   $ (RotateTransform.Angle)�6Z    06�   $ 	0:0:0:0.1D�6�   5�   #   5�      5�      :� 5�      $ PreviewMouseDown��6'   .[�5�      �� .��5�   "   �� .��5�   &   W� $ 
ImageScale��66   $ (ScaleTransform.ScaleX)�6Y    1.56�   $ 	0:0:0:0.1D�6�   W� 5�   &   $ 
ImageScale��66   $ (ScaleTransform.ScaleY)�6Y    1.56�   $ 	0:0:0:0.1D�6�   5�   #   5�      5�      :� 5�      $ PreviewMouseUp��6'   .[�5�      �� .��5�   "   �� .��5�   &   W� $ 
ImageScale��66   $ (ScaleTransform.ScaleX)�6Y    1.16�   $ 	0:0:0:0.1D�6�   W� 5�   &   $ 
ImageScale��66   $ (ScaleTransform.ScaleY)�6Y    1.16�   $ 	0:0:0:0.1D�6�   5�   #   5�      5�      5�      5�      5�      �� 5�      "  ��6   .�5�   
   ��  � ��$ Center=�60   �� 5�   
    � ��$ Center=�6.   �� 5�   
    � �� 5�      �� 5�      " ��6"   .R�5�      �� $ checkboxBorder��6   
������ ._�5�      �� $ checkboxImage��6    $7��1pack://application:,,,/Images/Options/shuffle.png��5�       $��15��5�       $��15��5�       $ .5,.5&�5�       $��Center=�5�       $��Center=�5�       $ HighQuality=�5�       $ Aliased=�5�       z�5�      V� 5�   "   .~�5�   &   �� 
ImageScale��$	 1Q�6I   $
 1Q�6T   �� 5�   &   ImageRotate��$ 0Q�6K   5�   #   5�      5�       5�      P� 5�       �@d�	IsChecked$
 TrueY�67   . �5�      �� $ checkboxBorder��6%    � ��
 �  ��6W   �� 5�      $ checkboxImage��6%    � ��$7 1pack://application:,,,/Images/Options/shuffle.png��6R   5�      :� 5�      $ 
MouseEnter��6'   .[�5�      �� .��5�   "   �� .��5�   &   W� $ 
ImageScale��66   $ (ScaleTransform.ScaleX)�6Y    1.16�   $ 
0:0:0:0.05D�6�   W� 5�   &   $ 
ImageScale��66   $ (ScaleTransform.ScaleY)�6Y    1.16�   $ 
0:0:0:0.05D�6�   5�   #   5�      5�      :� 5�      $ 
MouseLeave��6'   .[�5�      �� .��5�   "   �� .��5�   &   W� $ 
ImageScale��66   $ (ScaleTransform.ScaleX)�6Y    16�   $ 	0:0:0:0.1D�6�   W� 5�   &   $ 
ImageScale��66   $ (ScaleTransform.ScaleY)�6Y    16�   $ 	0:0:0:0.1D�6�   W� 5�   &   $ ImageRotate��66   $ (RotateTransform.Angle)�6Z    06�   $ 	0:0:0:0.1D�6�   5�   #   5�      5�      :� 5�      $ PreviewMouseDown��6'   .[�5�      �� .��5�   "   �� .��5�   &   W� $ 
ImageScale��66   $ (ScaleTransform.ScaleX)�6Y    1.56�   $ 	0:0:0:0.1D�6�   W� 5�   &   $ 
ImageScale��66   $ (ScaleTransform.ScaleY)�6Y    1.56�   $ 	0:0:0:0.1D�6�   5�   #   5�      5�      :� 5�      $ PreviewMouseUp��6'   .[�5�      �� .��5�   "   �� .��5�   &   W� $ 
ImageScale��66   $ (ScaleTransform.ScaleX)�6Y    1.16�   $ 	0:0:0:0.1D�6�   W� 5   &   $ 
ImageScale��66   $ (ScaleTransform.ScaleY)�6Y    1.16�   $ 	0:0:0:0.1D�6�   5  #   5     5     5     5	     5
     5     