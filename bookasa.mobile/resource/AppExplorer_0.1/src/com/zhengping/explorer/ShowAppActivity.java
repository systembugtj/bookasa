package com.zhengping.explorer;

import java.util.ArrayList;
import java.util.List;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.Window;
import android.view.WindowManager;
import android.view.View.OnClickListener;
import android.widget.BaseAdapter;
import android.widget.GridView;
import android.widget.ImageButton;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

public class ShowAppActivity extends Activity implements Runnable {
	
	private static final int SEARCH_APP = 0;
	private static final int DELETE_APP = 1;
	
	GridView gv;
	private List<PackageInfo> packageInfos;
	
	private List<PackageInfo> userPackageInfos;
	
	private ProgressDialog pd;
	
	ImageButton ib_change_category;
	
	private boolean allApplication = true;
	
	private Handler mHandler = new Handler(){

		@Override
		public void handleMessage(Message msg) {
			super.handleMessage(msg);
			if(msg.what == SEARCH_APP) {
				gv.setAdapter(new GridViewAdapter(ShowAppActivity.this,packageInfos));
				pd.dismiss();
				setProgressBarIndeterminateVisibility(false);
			}
			
			if(msg.what == DELETE_APP) {
				System.out.println("Delete App Success!!");
			}
			
		}
		
		
	};
	
    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        
       // requestWindowFeature(Window.FEATURE_NO_TITLE);
        requestWindowFeature(Window.FEATURE_INDETERMINATE_PROGRESS);
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN, WindowManager.LayoutParams.FLAG_FULLSCREEN);
        
        setContentView(R.layout.show_app_grid);
        setProgressBarIndeterminateVisibility(true);
        gv = (GridView) this.findViewById(R.id.gv_apps);
        ib_change_category = (ImageButton) this.findViewById(R.id.ib_change_category);
        ib_change_category.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View v) {
				if(allApplication) {
					ib_change_category.setImageResource(R.drawable.user);
					gv.setAdapter(new GridViewAdapter(ShowAppActivity.this,userPackageInfos));
					allApplication = false;
					Toast.makeText(ShowAppActivity.this, "用户安装的程序列表", Toast.LENGTH_LONG).show();
				} else {
					ib_change_category.setImageResource(R.drawable.all);
					gv.setAdapter(new GridViewAdapter(ShowAppActivity.this,packageInfos));
					allApplication = true;
					Toast.makeText(ShowAppActivity.this, "所有程序列表", Toast.LENGTH_LONG).show();
				}
				
			}});
        pd = ProgressDialog.show(this, "请稍候...", "正在搜索你所安装的应用程序...",true,false);
        Thread t = new Thread(this);
        t.start();
        
    }
    
    
    
    class GridViewAdapter extends BaseAdapter {
    	
    	LayoutInflater inflater;
    	List<PackageInfo> pkInfos;
    	
    	public GridViewAdapter(Context context,List<PackageInfo> packageInfos) {
    		inflater = LayoutInflater.from(context);
    		this.pkInfos = packageInfos;
    	}

		@Override
		public int getCount() {
			// TODO Auto-generated method stub
			return pkInfos.size();
		}

		@Override
		public Object getItem(int arg0) {
			// TODO Auto-generated method stub
			return pkInfos.get(arg0);
		}

		@Override
		public long getItemId(int position) {
			// TODO Auto-generated method stub
			return position;
		}

		@Override
		public View getView(int position, View convertView, ViewGroup parent) {
			View view = inflater.inflate(R.layout.gv_item, null);
			TextView tv = (TextView) view.findViewById(R.id.gv_item_appname);
			ImageView iv = (ImageView) view.findViewById(R.id.gv_item_icon);
			
			//tv.setText(packageInfos.get(position).packageName);
			tv.setText(pkInfos.get(position).applicationInfo.loadLabel(getPackageManager()));
			
			iv.setImageDrawable(pkInfos.get(position).applicationInfo.loadIcon(getPackageManager()));
			
			return view;
		}
    	
    }


	@Override
	public void run() {
		packageInfos = getPackageManager().getInstalledPackages(PackageManager.GET_UNINSTALLED_PACKAGES);
		userPackageInfos = new ArrayList<PackageInfo>();
		for(int i=0;i<packageInfos.size();i++) {
			
			PackageInfo temp = packageInfos.get(i);
			ApplicationInfo appInfo = temp.applicationInfo;
			boolean flag = false;
			if((appInfo.flags & ApplicationInfo.FLAG_UPDATED_SYSTEM_APP) != 0) {
				 // Updated system app
				flag = true;
			} else if((appInfo.flags & ApplicationInfo.FLAG_SYSTEM) == 0) {
				// Non-system app
				flag = true;
			}
			if(flag) {
				userPackageInfos.add(temp);
			}
		}
		
		try {
			Thread.currentThread().sleep(2000);
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		mHandler.sendEmptyMessage(SEARCH_APP);
		
		try {
			Thread.currentThread().sleep(5000);
			mHandler.sendEmptyMessage(DELETE_APP);
		} catch (InterruptedException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
	}
    
}