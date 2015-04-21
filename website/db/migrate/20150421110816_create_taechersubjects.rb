class CreateTaechersubjects < ActiveRecord::Migration
  def change
    create_table :taechersubjects do |t|
      t.belongs_to :teacher 
      t.belongs_to :subject 
      t.boolean :verified, default: false

      t.timestamps null: false
    end
    
  end
end
