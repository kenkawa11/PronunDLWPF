   M S B A M L   `   `   `  ����F  BPronunDLWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:PronunDLWPFPronunDLWPF  5   	       PronunDLWPF.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:PronunDLWPF     5   	   -   5          � Title$  Pronounce Downloader��5   	   $	��300��6&   $	��500��63    � Icon$ 	/icon.png��6{    � 	Resources 5	       �� MergedDictionaries �� Source�� 5
   
    5      �� 5      $e _pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml?�6%   �� 5      $h bpack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml?�6%   �� 5      $z tpack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.DeepPurple.xaml?�6%   �� 5      $s mpack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Accent/MaterialDesignColor.Lime.xaml?�6%   %    5      .��5      � ���	#FFDBEBEA � ColumnDefinitions 5   
   �� 5      �� 5      $��9* �6   �� 5      $	��10* �6    � RowDefinitions 5   
   �� 5      $	��29* �6   �� 5      $	��85* �6   �� 5      $	��57* �6   �� 5       $	��57* �6   �� 5!      $	��57* �6   .+�5$   
   �� -   5        $��Select file��5$      $
��Left=�6'   $��40��6B   $��93,0,0,0q�6N   $��Center=�6`   $	��120��6{   $��Arial Black)�6�   $��16(�6�   $��2��6�   $��1��6�   �� 5%   
   -   5         ��Name$ txtifile��5%      $
��Left=�68   $��40��6S   $��15,0,0,0q�6_    �� TextWrapping$
 Wrap=�6q   $��Center=�6�   $	��300��6�   $��3��6�   ���	#FFEFF6F7$��1��6�   ��6$   �� *Fn+6�   �� 5&   
   -   5        	 ��Name$	 doneBtn��5&      $
��Done��6"   $
��Left=�61   $��40��6L   $	��Top=�6X   $	��100��6p   $��16(�6�   $��Arial Black)�6�   $��2��6�   
 J� RenderTransformOrigin$
 0.462,-1.489&�6�   $��50,6,0,0q�6�   $��1��6�   �� 5'   
   -   5        $��Cancel��5'      $
��Left=�6"   $��40��6=   $��45,6,0,0q�6I   $	��Top=�6[   $	��130��6s   $��16(�6�   $��Arial Black)�6�   $��2��6�   $��2��6�   �� 5(   
   -   5         ��Name$ label1��5(      $
��Left=�6;   $��35��6V   $��	150,0,0,0q�6b   $��Center=�6u   $	��152��6�   $
 0.781,1.001&�6�   $��4��6�   $��Arial Black)�6�   $��20(�6�   $��2��6�   $��1��6  ��6    �� *Status+6  �� 5)   
   -   5        $ label2��5)      $
��Left=�6=   $��35��6X   $��	131,9,0,0q�6d   $	��Top=�6w   $	��113��6�   $��3��6�   $��20(�6�   $��Arial Black)�6�   $��2��6�   ��6    �� *
Progress+6�   � 5*   
   $
��Left=�64   $��20��6O   $��91,0,0,0q�6[   $��Center=�6m   $	��250��6�    � Minimum$ 0Q�6�    � Maximum$	 100Q�6�   $��2��6�   $��3��6�   $��1��6�    � Value 6   �� *BarProgress+6�   �� 5+   
   $��Progress��6   $
��Left=�6#   $��40��6>   $��6,11,0,0q�6J   $	��Top=�6\   $	��120��6t   $��20(�6�   $��Arial Black)�6�   $��3��6�   $��2��6�   5,      5-      